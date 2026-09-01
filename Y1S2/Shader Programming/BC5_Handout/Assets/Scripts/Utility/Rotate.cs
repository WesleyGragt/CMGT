using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 axis=new Vector3(0,1,0);
	public float rotationSpeedDegreesPerSecond = 90;

    void Update()
    {
		transform.Rotate(axis, rotationSpeedDegreesPerSecond * Time.deltaTime);
    }
}
