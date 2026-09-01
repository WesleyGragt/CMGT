using System;
using System.Text;

namespace MathTools {
	public class Vector {
		public readonly int length;
		double[] data;
		public bool row=true;

		public Vector(int pLength) {
			length = pLength;
			data = new double[length];
		}

		/*
		public Vector(double[] values) {
			length = values.Length;
			data=(double[])(values.Clone());
		}*/

		public Vector(params double[] values) {
			length = values.Length;
			data = (double[])values.Clone ();
		}

		public double this[int index] {
			get {
				return data [index];
			}
			set {
				data [index] = value;
			}
		}

		public override string ToString ()
		{
			if (row) {
				StringBuilder output = new StringBuilder ();
				output.Append ("[");
				for (int c = 0; c < length; c++) {
					output.Append (String.Format ("{0,5:F2}", data [c]));
					output.Append ("\t");
				}
				output.Append ("]");
				return output.ToString();
			} else {
				StringBuilder output = new StringBuilder ();
				for (int c = 0; c < length; c++) {
					output.Append ("[");
					output.Append (String.Format ("{0,8:F2}", data [c]));
					output.Append ("]\n");
				}
				return output.ToString();
			}
		}
	}
}