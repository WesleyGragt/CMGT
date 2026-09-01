using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlMouse : MonoBehaviour
{
	public Space space;
	public float yRotationSpeed = 1;
	public float xRotationSpeed = 1;
	public float maxXDegrees = 60;
	public float minXDegrees = -60;

	float xRot = 0;
	float yRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update() {
		xRot = Mathf.Clamp(xRot + Input.GetAxis("Mouse Y") * xRotationSpeed, minXDegrees, maxXDegrees);
		yRot += Input.GetAxis("Mouse X") * yRotationSpeed;
		if (space == Space.Self) {
			transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
		} else {
			transform.rotation = Quaternion.Euler(xRot, yRot, 0);
		}
	}
}
