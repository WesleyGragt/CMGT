using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMouseControl : MonoBehaviour
{
	public float coefficientOfReflection = 0.98f;
	public bool CustomCollision = true;
	Vector3 lastPosition=Vector3.zero;
	Vector3 velocity;


    void Update()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
			transform.position = hitInfo.point;
		}
    }
	private void FixedUpdate() {
		velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
		lastPosition = transform.position;
	}

	private void OnCollisionEnter(Collision collision) {
		// Exercise 5
		//  TODO: 
		//   If we collide with something that has a TrackVelocity component,
		//   calculate the relative velocity of the collision,
		//   and reflect it in the collision normal (see collision.contacts[0].normal).
	}
}
