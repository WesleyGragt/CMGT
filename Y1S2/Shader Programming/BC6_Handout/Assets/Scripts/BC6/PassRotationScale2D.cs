using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassRotationScale2D : MonoBehaviour
{
	public Transform target;
	public Vector3 positionOffset;
	Material custom2DTransform;

    // Start is called before the first frame update
    void Start()
    {
		custom2DTransform = GetComponent<MeshRenderer>().material; // Should contain shader of type Transformation2D
    }

    // Update is called once per frame
    void Update()
    {
		custom2DTransform.SetFloat("_Rotation", target.rotation.eulerAngles.z);
		// Passing a 3D vector to a float4 in the shader, where we'll use only the xy coordinates:
		//  (It ain't pretty, but it works)
		custom2DTransform.SetVector("_Scale", target.localScale);
		custom2DTransform.SetVector("_Translation", target.position + positionOffset);
    }
}
