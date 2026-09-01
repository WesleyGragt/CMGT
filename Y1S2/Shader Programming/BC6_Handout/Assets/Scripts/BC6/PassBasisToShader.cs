using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBasisToShader : MonoBehaviour
{
	Material transformation3D;
	public GetVector right;
	public GetVector up;
	public GetVector forward;
	
    // Start is called before the first frame update
    void Start()
    {
		transformation3D = GetComponent<MeshRenderer>().material; // Should be a material based on the Transformation3DBasisVectors shader
    }

    // Update is called once per frame
    void Update()
    {
		// Note: we pass a Vector3 to a float4 shader uniform.
		//  That ain't pretty, but it works. (w=0)
		transformation3D.SetVector("_Right", right.vector);
		transformation3D.SetVector("_Up", up.vector);
		transformation3D.SetVector("_Forward", forward.vector);
	}
}
