using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Destroying bullet");
		Destroy(gameObject);
	}
}
