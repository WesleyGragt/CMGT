using UnityEngine;

public class Enemy : MonoBehaviour
{
	private float xSpeed;           // Randomly chosen on Start
	[Range(0f, 100f)]               // Force positive
	public float ySpeed = 1.0f;     // The speed along along the negative world y-axis.
	public Vector2 bounds;          // x indicates -x..x range, y indicates -y..y range

	private void Start()
	{
		//pick a random xspeed based on the provided yspeed
		xSpeed = Random.Range(-ySpeed, ySpeed);
	}

	private void Update()
	{
		//Move the enemy in world space!
		transform.Translate(new Vector3(xSpeed, -ySpeed) * Time.deltaTime, Space.World);

		// Check if the enemy has moved out of the game bounds.
		if  (
			transform.position.x < -bounds.x || transform.position.x > bounds.x || 
			transform.position.y < -bounds.y || transform.position.y > bounds.y
			)
		{
			Debug.Log("Destroying enemy out of bounds:" + transform.position);
			// Destroy the enemy game object if it is out of bounds.
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(-bounds.x, -bounds.y, 0), new Vector3(bounds.x, -bounds.y, 0));
		Gizmos.DrawLine(new Vector3(bounds.x, -bounds.y, 0), new Vector3(bounds.x, bounds.y, 0));
		Gizmos.DrawLine(new Vector3(bounds.x, bounds.y, 0), new Vector3(-bounds.x, bounds.y, 0));
		Gizmos.DrawLine(new Vector3(-bounds.x, bounds.y, 0), new Vector3(-bounds.x, -bounds.y, 0));
	}

}
