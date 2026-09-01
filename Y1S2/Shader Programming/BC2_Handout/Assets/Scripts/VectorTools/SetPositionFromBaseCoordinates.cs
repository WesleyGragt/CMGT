using UnityEngine;

// --- Purpose:
// Demonstrates how a position can be expressed as coordinates relative to basis vectors.
// --- Usage:
// In the inspector, set coordinates relative to a basis.
// In the BaseVectors component, add references to three basis vectors.
// Then, in play mode, rotate and scale the basis vectors, and see how the position of this game object changes accordingly.
[RequireComponent(typeof(BasisVectors))]
public class SetPositionFromBaseCoordinates : MonoBehaviour
{
	public Vector3 BaseCoordinates;

	GetVector XVec;
	GetVector YVec;
	GetVector ZVec;

    void Start()
    {
		var bv = GetComponent<BasisVectors>();
		XVec=bv.XVector;
		YVec=bv.YVector;
		ZVec=bv.ZVector;
    }

    void Update()
    {
		transform.position =
			BaseCoordinates.x * XVec.vector +
			BaseCoordinates.y * YVec.vector +
			BaseCoordinates.z * ZVec.vector;
    }
}
