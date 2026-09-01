using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
	public Transform target;
	public float visionAngle;
	public float turnSpeedDegreesSecond;
	public float moveSpeed;
	public bool inSight = false;
	public MeshRenderer eye;
	public float angleCos; // calculated
	public float angle;

    void Start()
    {
    }

    void Update()
    {
		// Exercise 0:
		// TODO: Make this enemy chase the player, with constant speed, if the player is in range:


		// Exercise 1:
		// TODO: use the dot product to compute whether the target is actually in sight,
		//  considering the visionAngle!
		Vector3 targetPos = target.position - transform.position;
		Vector3 toTarget = targetPos;
        float dotProduct = Vector2.Dot(new Vector2(targetPos.x, targetPos.z), new Vector2(transform.forward.x, transform.forward.z));

		float targetL = Mathf.Sqrt((targetPos.x * targetPos.x) + (targetPos.z * targetPos.z));
		float enemyL = Mathf.Sqrt((transform.forward.x * transform.forward.x) + (transform.forward.z * transform.forward.z));

		angleCos = Mathf.Clamp(dotProduct / (targetL * enemyL), -1f, 1f);
		angle = Mathf.Acos(angleCos) * (180/Mathf.PI);


		inSight = angle < 1; // about 50 degrees?

		if (inSight) { 
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toTarget), turnSpeedDegreesSecond * Time.deltaTime);

			eye.material.EnableKeyword("_EMISSION");
		} else {
			eye.material.DisableKeyword("_EMISSION");
		}
	}
}
