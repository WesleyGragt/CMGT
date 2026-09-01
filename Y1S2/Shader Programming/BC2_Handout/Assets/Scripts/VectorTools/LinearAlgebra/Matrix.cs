using System;
using System.Text;
using System.Collections.Generic;

namespace MathTools {
	public class Matrix {
		public readonly int rows;
		public readonly int cols;

		double[,] data;

		public Matrix(int pRows, int pCols) {
			rows = pRows;
			cols = pCols;
			data = new double[rows,cols];
		}

		public Matrix(double[,] pData) {
			data = (double[,])(pData.Clone ());
			rows = data.GetLength (0);
			cols = data.GetLength (1);
		}

		public Matrix(int pRows, int pCols, params double[] values) {
			rows = pRows;
			cols = pCols;
			data = new double[rows,cols];
			for (int i = 0; i < values.Length; i++) {
				if (i >= rows * cols)
					break;
				data [i / cols, i % cols] = values [i];
			}
		}

		public static Matrix Identity(int dim) {
			Matrix I = new Matrix (dim, dim);
			for (int i = 0; i < dim; i++) {
				I[i,i]=1;
			}
			return I;
		}

		public static Matrix FromColumns(params Vector[] columns) {
			Matrix result = new Matrix(columns[0].length, columns.Length);
			for (int row = 0; row<result.rows; row++) {
				for (int col = 0; col<result.cols; col++) {
					if (columns[col].length!=result.rows) {
						throw new InvalidOperationException("When initializing a matrix from vectors, all vectors need to have the same length");
					}
					result[row, col]=columns[col][row];
				}
			}
			return result;
		}

		public static Matrix FromRows(params Vector[] rows) {
			Matrix result = new Matrix(rows.Length, rows[0].length);
			for (int row = 0; row<result.rows; row++) {
				for (int col = 0; col<result.cols; col++) {
					if (rows[row].length!=result.cols) {
						throw new InvalidOperationException("When initializing a matrix from vectors, all vectors need to have the same length");
					}
					result[row, col]=rows[row][col];
				}
			}
			return result;
		}

		public Matrix Clone() {
			return new Matrix (data);
		}

		public Matrix Transpose() {
			Matrix trans = new Matrix (cols, rows);
			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < cols; c++) {
					trans.data [c, r] = data [r, c];
				}
			}
			return trans;
		}

		public Matrix SubMatrix(int startRow, int startCol, int rows, int cols) {
			Matrix result = new Matrix (rows, cols);
			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < cols; c++) {
					result [r, c] = this [startRow + r, startCol + c];
				}
			}
			return result;
		}

		public static Matrix ConcatenateColumn(Matrix A, Matrix B) {
			if (A.rows!=B.rows)
				throw new InvalidOperationException("Only matrices with the same number of rows can be concatenated");
			Matrix result = new Matrix (A.rows, A.cols + B.cols);
			for (int r = 0; r < A.rows; r++) {
				for (int c = 0; c < A.cols; c++) {
					result [r, c] = A [r, c];
				}
				for (int c = 0; c < B.cols; c++) {
					result [r, c + A.cols] = B [r, c];
				}
			}
			return result;
		}

		public static Matrix ConcatenateColumn(Matrix A, Vector v) {
			if (A.rows != v.length)
				throw new InvalidOperationException ("Matrix rows and vector length must match for concatenation");
			Matrix result = new Matrix (A.rows, A.cols + 1);
			for (int r = 0; r < A.rows; r++) {
				for (int c = 0; c < A.cols; c++) {
					result [r, c] = A [r, c];
				}
				result [r, A.cols] = v[r];
			}
			return result;
		}

		public double this[int row, int col] {
			get {
				return data [row, col];
			}
			set {
				data [row, col] = value;
			}
		}

		public override string ToString ()
		{
			StringBuilder output = new StringBuilder ();
			for (int r = 0; r < rows; r++) {
				output.Append ("[");
				for (int c = 0; c < cols; c++) {
					output.Append(String.Format("{0,5:F2}",data[r,c]));
					output.Append ("\t");
				}
				output.Append ("]\n");
			}
			return output.ToString ();
		}

		public Matrix Inverse() {
			if (rows != cols)
				throw new InvalidOperationException ("The inverse is only defined for square matrices");
			Matrix combined = ConcatenateColumn (this, Matrix.Identity (rows));
			combined.Sweep (true);
			if (combined.DiagonalProduct == 0)
				return null; // no inverse
			return combined.SubMatrix(0,cols,rows,cols);
		}

		public double Determinant() {
			if (rows != cols)
				throw new InvalidOperationException ("The determinant is only defined for square matrices");
			Matrix C = Clone ();
			double multiplier = C.Sweep (false);
			return multiplier * C.DiagonalProduct;
		}

		public int Rank() {
			Matrix C = Clone ();
			int rank;
			double multiplier = C.Sweep (out rank, false);
			return rank;
		}

		public double Sweep(bool full=true) {
			int rank;
			return Sweep (out rank, full);
		}

		// This counts as close enough to zero, for rank/determinant/kernel computation (see Sweep method)
		const double epsilon = 0.00001;

		// Applies Gaussian elimination if full==false, and Gauss-Jordan elimination if full=true.
		// Returns the "determinant multiplier" given by the applied row operations.
		public double Sweep(out int rank, bool full=true) {
			double detMultiplier = 1;
			int c = 0; // current column
			int r = 0; // the number of rows on top that won't be touched (if full==false)
			while (c < cols) {
				// find best pivot (choose maximum value for best numerical stability):
				double max=0;
				int argMax = 0;
				for (int rw = r; rw < rows; rw++) {
					if (Math.Abs (data [rw, c]) > max) {
						max = Math.Abs (data [rw, c]);
						argMax = rw;
					}
				}
				if (max <= epsilon) {
					// empty column (at least below the row [r]) - no sweep
					c++;
					continue;
				}
				if (argMax > r) { // swap rows
					detMultiplier *= -1;
					for (int cl = c; cl < cols; cl++) {
						double tmp = data [r, cl];
						data [r, cl] = data [argMax, cl];
						data [argMax, cl] = tmp;
					}
				}
				// sweep:
				int startRow=full?0:r+1;
				for (int rw = startRow; rw < rows; rw++) {
					if (rw == r)
						continue;
					double m = data [rw, c] / data [r, c]; // multiplier for row operation
					data[rw,c] = 0;
					for (int cl = c + 1; cl < cols; cl++) {
						data [rw, cl] -= data [r, cl] * m;
						if (Math.Abs(data[rw, cl])<epsilon) { // dealing with floating point rounding errors
							data[rw, cl]=0;
						}
					}
				}
				if (full) { // normalize pivot row
					detMultiplier *= data[r,c];
					for (int cl = cols - 1; cl > c; cl--) {
						data [r, cl] /= data [r, c];
					}
					data [r, c] = 1;
				}
				//Console.WriteLine ("After pivot on row={0} col={1}:\n{2}\nDeterminant multiplier: {3}",r,c,this,detMultiplier);

				r++;
				c++;
			}
			rank = r;
			return detMultiplier;
		}

		public double DiagonalProduct {
			get {
				if (rows > cols)
					throw new InvalidOperationException ("The diagonal is only defined for square matrices");
			
				double product = 1;

				for (int i = 0; i < rows; i++) {
					product *= data [i, i];
				}
				return product;
			}
		}

		public Vector[] KernelRow() {
			return Transpose().KernelColumn();
		}

		public Vector[] KernelColumn() {
			var result = new List<Vector>();
			Matrix C = Clone();
			C.Sweep();
			int col = 0;
			int row = 0;
			Console.WriteLine("Kernel computation. Standard form:\n"+C);
			while (col<cols) {
				if (row>=rows || C.FirstNonZeroColumn(row)>col) {
					Vector v = new Vector(C.cols);
					v[col]=1;
					for (int r = 0; r<row; r++) {
						if (C[r, col]!=0) {
							v[C.FirstNonZeroColumn(r)]=-C[r, col];
						}
					}
					Console.WriteLine("Kernel vector for row={0} col={1}:\n {2}",row,col,v);
					result.Add(v);
					col++;
				} else {
					row++;
					col++;
				}
			}
			return result.ToArray();
		}

		public Vector SolveRow(Vector outcome) {
			Matrix T = Transpose();
			return T.SolveColumn(outcome);
		}

		public Vector SolveColumn(Vector outcome) {
			Matrix A = ConcatenateColumn (this, outcome);
			int rank;
			A.Sweep (out rank);
			Vector result = new Vector (cols);
			Console.WriteLine ("Linear solving, augmented matrix:\n"+A);
			for (int i = 0; i < rows; i++) {
				int pivotCol = A.FirstNonZeroColumn (i);
				Console.WriteLine ("row={0}, pivot column={1} current result={2}",i,pivotCol,result);
				if (pivotCol == cols)
					return null; // no solution!
				if (pivotCol > cols)
					return result; // early exit
				result[pivotCol]=A[i,cols];
			}
			return result;
		}

		public int FirstNonZeroColumn(int row) {
			int col = 0;
			while (col < cols && data [row, col] == 0) {
				col++;
			}
			return col;
		}



		public static Matrix operator *(Matrix Left, Matrix Right) {
			if (Left.cols != Right.rows)
				throw new InvalidOperationException ("For matrix multiplication, the number of columns (left) and rows (right) must match");
			Matrix result = new Matrix (Left.rows, Right.cols);
			for (int r = 0; r < result.rows; r++) {
				for (int c = 0; c < result.cols; c++) {
					// compute dot products of left-row and right-col:
					double dot=0;
					for (int i = 0; i < Left.cols; i++) {
						dot += Left [r, i] * Right [i, c];
					}
					result [r, c] = dot;
				}
			}
			return result;
		}

		// multiply a matrix with a column vector:
		public static Vector operator *(Matrix A, Vector v) {
			if (A.cols != v.length)
				throw new InvalidOperationException ("For matrix/column vector multiplication, the number of matrix columns must match the vector length");
			Vector result = new Vector (A.rows);
			for (int r = 0; r < result.length; r++) {
				// compute dot products of left-row and right-col:
				double dot=0;
				for (int i = 0; i < A.cols; i++) {
					dot += A [r, i] * v [i];
				}
				result [r] = dot;
			}
			return result;
		}

		// multiply a row vector with a matrix:
		public static Vector operator *(Vector v, Matrix A) {
			if (A.rows != v.length)
				throw new InvalidOperationException ("For row vector/matrix multiplication, the number of matrix rows must match the vector length");
			Vector result = new Vector (A.cols);
			for (int r = 0; r < result.length; r++) {
				// compute dot products of left-row and right-col:
				double dot=0;
				for (int i = 0; i < A.rows; i++) {
					dot += v[i] * A [i,r];
				}
				result [r] = dot;
			}
			return result;
		}
	}
}