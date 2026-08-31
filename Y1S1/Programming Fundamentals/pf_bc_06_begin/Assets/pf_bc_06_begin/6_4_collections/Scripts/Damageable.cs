using UnityEngine;

// Same as Damageable_Final
// Note: there is a very subtle issue with this code, which we'll discuss in the next bootcamp.

public class Damageable : MonoBehaviour
{
	[Min(1)]
	public int health = 1;
	
	public GameObject explosionPrefab;

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Ouch, I was hit by "+collision.gameObject.name);

		--health;

		if (health <= 0)
		{
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);  
			GameManager.instance.ObjectWasDestroyed(gameObject);

			Destroy(gameObject);
		}
	}
}
