using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCannon : MonoBehaviour
{
	public Transform target;
	public Transform barrel;
	public Transform barrelPivot;
	public GameObject bullet;

	private bool targetVisible = false;

	private Quaternion cannonRot;
	private Quaternion pivotRot;

	public float timer = 5;
	private float timeAdd;

    private void Start()
    {
		timeAdd = timer;
    }

    void Update()
    {
		targetVisible = SeesTarget();

		if (targetVisible) {
			
			Vector3 angles = Quaternion.LookRotation(target.position - transform.position, Vector3.up).eulerAngles;
			cannonRot = Quaternion.Euler(0, angles.y, 0);
			pivotRot = Quaternion.Euler(angles.x, 0, 0);

			if (timer < 0)
			{
				Instantiate(bullet, barrel.position, Quaternion.Euler(angles));
				timer = timeAdd;
			} else
			{
				timer -= 0.1f;
			}
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, cannonRot, .02f);
        barrelPivot.localRotation = Quaternion.Lerp(barrelPivot.localRotation, pivotRot, .02f);
    }

	private bool SeesTarget()
	{
		if (target == null) return false;

		Ray ray = new Ray(barrel.position, target.position - barrel.position);

		if (Physics.Raycast(ray, out RaycastHit hitInfo))
		{
			if (hitInfo.transform == target) return true;
			return false;
		}

		return false;
	}

    private void OnDrawGizmos()
    {
		if (targetVisible) Gizmos.color = Color.green;
		else Gizmos.color = Color.red;
		
		Gizmos.DrawLine(barrel.position, target.position);
    }
}
