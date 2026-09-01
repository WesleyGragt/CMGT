using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 axis=new Vector3(0,1,0);
	public float rotationSpeedDegreesPerSecond = 90;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
		transform.Rotate(axis, rotationSpeedDegreesPerSecond * Time.deltaTime);

        float forwards = Mathf.PingPong(Time.time, axis.magnitude);
        transform.position = startPos + (Vector3.forward * forwards);
    }
}
