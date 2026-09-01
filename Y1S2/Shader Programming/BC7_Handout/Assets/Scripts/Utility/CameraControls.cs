using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	public float mouseSpeed = 1;
	public float moveSpeedSecond = 10;
	const float maxMouseDelta = 2;

	float xRot, yRot;

	// Start is called before the first frame update
	void Start() {
		Vector3 currentRot = transform.rotation.eulerAngles;
		xRot = currentRot.x;
		yRot = currentRot.y;
	}

	// Update is called once per frame
	void Update() {
		float mx = Mathf.Clamp(Input.GetAxis("Mouse X"),-maxMouseDelta,maxMouseDelta);
		float my = Mathf.Clamp(Input.GetAxis("Mouse Y"),-maxMouseDelta,maxMouseDelta);

		if (Input.GetMouseButton(1)) {
			xRot -= my * mouseSpeed;
			yRot += mx * mouseSpeed;
		}

		transform.rotation = Quaternion.Euler(xRot, yRot, 0);

		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");
		float dz = (Input.GetKey(KeyCode.Q) ? -1 : 0) + (Input.GetKey(KeyCode.E) ? 1 : 0);
		float speed = moveSpeedSecond * Time.deltaTime * (Input.GetKey(KeyCode.LeftShift) ? 3 : 1);
		transform.position +=
			(transform.forward * dy + transform.right * dx + transform.up * dz) * speed;
	}
}
