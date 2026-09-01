using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassEulerAnglesToShader : MonoBehaviour
{
	public Transform target;
	Material customRotation;
	Vector3 offset;

    void Start()
    {
		customRotation = GetComponent<MeshRenderer>().material;
		offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 euler = target.rotation.eulerAngles;
		customRotation.SetFloat("_Pitch", euler.x);
		customRotation.SetFloat("_Heading", euler.y);
		customRotation.SetFloat("_Bank", euler.z);

		// Passing Vector3s to Vector4 shader properties - ugly, but it works:
		customRotation.SetVector("_Translation", target.position + offset);
		customRotation.SetVector("_Scale", target.lossyScale);
	}
}
