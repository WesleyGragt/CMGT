using UnityEngine;
using UnityEngine.UI;

using MathTools; // Custom linear algebra classes (Matrix, Vector)

// --- Purpose:
// Shows how dot product can sometimes be used to compute coordinates of a gameobject, relative to a given base.
// ("Sometimes" means: it only works correctly when the base is orthonormal!)
// --- Usage:
// In the inspector, set references to UI elements with a Text component.
// In the BaseVectors component, add references to three base vectors.
// Then, in play mode, rotate and scale the base vectors, 
// see how the computed coordinates change, and check when they are correct.
[RequireComponent(typeof(BasisVectors))]
public class ComputeCoordinates : MonoBehaviour
{
	public Text CoordinatesDisplay;
	public Text DotProductsDisplay;
	public Text VolumeDisplay;

	GetVector XVec;
	GetVector YVec;
	GetVector ZVec;

    void Start()
    {
		var baseComp = GetComponent<BasisVectors>();
		XVec=baseComp.XVector;
		YVec=baseComp.YVector;
		ZVec=baseComp.ZVector;
    }

    void Update()
    {
		var vx = new Vector(XVec.vector.x, XVec.vector.y, XVec.vector.z);
		var vy = new Vector(YVec.vector.x, YVec.vector.y, YVec.vector.z);
		var vz = new Vector(ZVec.vector.x, ZVec.vector.y, ZVec.vector.z);
		Matrix A = Matrix.FromColumns(vx, vy, vz);
		double det = A.Determinant();

		VolumeDisplay.text = string.Format("Volume: {0:F}", det);

		var coord = A.SolveColumn(new Vector(transform.position.x, transform.position.y, transform.position.z));
		if (coord!=null) {
			CoordinatesDisplay.text = string.Format("Coordinates:\n{0:F}\n{1:F}\n{2:F}", coord[0], coord[1], coord[2]);
		} else {
			CoordinatesDisplay.text = "Coordinates:\nNO SOLUTION!";
		}

		float dotX = Vector3.Dot(XVec.vector, transform.position);
		float dotY = Vector3.Dot(YVec.vector, transform.position);
		float dotZ = Vector3.Dot(ZVec.vector, transform.position);
		DotProductsDisplay.text = string.Format("Dot Products:\n{0:F}\n{1:F}\n{2:F}", dotX, dotY, dotZ);
    }
}