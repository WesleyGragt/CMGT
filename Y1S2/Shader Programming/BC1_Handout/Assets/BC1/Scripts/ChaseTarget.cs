using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ChaseTarget : MonoBehaviour
{
	public Transform target;
	public float visionAngle=45;
	public float chaseRange=5;
	public float turnSpeedDegreesSecond;
	public float moveSpeedPerSecond;
	public MeshRenderer eye;

    void Start()
    {
		SetEyeColor(true);
	}

	void SetEyeColor(bool enabled) {
		if (enabled) {
			eye.material.EnableKeyword("_EMISSION");
		} else {
			eye.material.DisableKeyword("_EMISSION");
		}
	}

    void Update()
    {
        Vector3 vecPos = target.position - transform.position;

        float targetAngle = Mathf.Atan2(vecPos.x, vecPos.z) * Mathf.Rad2Deg;
		float length = vecPos.magnitude;

		vecPos.y = 0;

		transform.Translate(Vector3.forward * length * moveSpeedPerSecond * Time.deltaTime);

		/*float currentAngle = transform.eulerAngles.y;
		float angle = currentAngle - targetAngle;
		if (angle < 180) angle += turnSpeedDegreesSecond * Time.deltaTime;
		else angle -= turnSpeedDegreesSecond * Time.deltaTime;*/

        transform.eulerAngles = new Vector3(0, targetAngle, 0);

		// TODO: 
		//  DONE - Make the enemy chase the player (target)
		//  - Make the enemy (slowly) rotate towards the player
		//  - Only do this when the player is in range (max distance) and in sight (max angle)
		//  - Draw rays in the scene view to indicate the vision range.
		// See the slides for details!
		// (You can use SetEyeColor for visual debugging)
	}
}
