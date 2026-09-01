using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassEulerAnglesToShader : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public Vector3 scale;
	[Space]	
	public Vector3 rotationAxis = new Vector3(0, 1, 0);
	public float rotationSpeed;
	public bool pingpong = false;
	public Vector3 rotationLength;

	Material customRotation;

	void Start()
    {
		customRotation = target.GetComponent<MeshRenderer>().material;
    }

	Vector3 euler = Vector3.zero;
    void Update()
    {
		if (!pingpong)
		{
			euler += rotationAxis * rotationSpeed * Time.deltaTime;
		} else
		{
			euler.x = Mathf.PingPong(Time.time, rotationLength.x) - rotationLength.x/2;
			euler.y = Mathf.PingPong(Time.time, rotationLength.y) - rotationLength.y / 2;
			euler.z = Mathf.PingPong(Time.time, rotationLength.z) - rotationLength.z / 2;

			euler *= 10;
		}

		customRotation.SetFloat("_Pitch", euler.x);
		customRotation.SetFloat("_Heading", euler.y);
		customRotation.SetFloat("_Bank", euler.z);

		customRotation.SetVector("_Translation", target.position + offset);
		customRotation.SetVector("_Scale", scale);
	}
}
