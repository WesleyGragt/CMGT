using UnityEngine;

public class Stabilizer : MonoBehaviour
{
	public float rotVectorScalar = 10;
	Rigidbody rb;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
		Vector3 wantedDir = Vector3.up;
		Vector3 currentDir = transform.up;

        Vector3 axis = Vector3.Cross(currentDir, wantedDir);

		rb.AddTorque(axis * rotVectorScalar);
    }
}
