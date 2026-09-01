using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarControls : MonoBehaviour
{
	public float steeringDegreesPerSecond;
	public float accelerationForce;
	public TextMeshProUGUI text;
	Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = Vector3.down * 0.5f; // stabilize!
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0, steeringDegreesPerSecond * Input.GetAxis("Horizontal") * Time.deltaTime, 0, Space.Self);
    }

	void FixedUpdate() {
		rb.AddForce(transform.forward * accelerationForce * Input.GetAxis("Vertical"));
		// Exercise 3
		// TODO: 
		// -Split the rigidbody's velocity up into right, up and forward velocity.
		//  Show their lengths on the given UI element [text].
		// -Make this "hovercraft" into a "car" by cancelling the sideways velocity every frame.
	}
}
