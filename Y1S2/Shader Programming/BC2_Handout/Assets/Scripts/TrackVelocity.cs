using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackVelocity : MonoBehaviour
{
	Rigidbody rb;
	Vector3 lastVelocity;
	Vector3 startPos;

	public Vector3 GetVelocityBeforeImpact() {
		return lastVelocity;
	}

    void Start()
    {
		rb = GetComponent<Rigidbody>();
		startPos = transform.position;
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			transform.position = startPos;
			rb.linearVelocity = Vector3.zero;
		}
	}

	void FixedUpdate()
    {
		lastVelocity = rb.linearVelocity;
    }
}
