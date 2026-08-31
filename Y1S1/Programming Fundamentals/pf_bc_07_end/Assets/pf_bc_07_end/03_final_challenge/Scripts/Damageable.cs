using UnityEngine;

namespace pf_bc_07_end_03_final_challenge
{
    // Same
    public class Damageable : MonoBehaviour
    {
    	[Min(1)]
    	public int health = 1;
	
    	public GameObject explosionPrefab;
        private bool alreadyDestroyed = false;

    	private void OnCollisionEnter (Collision collision)
    	{
            // Ignore any OnCollisionEnter calls if we are already destroyed
            if (alreadyDestroyed) return;

    		Debug.Log("Ouch, I was hit by "+collision.gameObject.name);

    		--health;

    		if (health <= 0)
    		{
    			Instantiate(explosionPrefab, transform.position, Quaternion.identity);  
    			GameManager.instance.ObjectWasDestroyed(gameObject);

    			Destroy(gameObject);

                alreadyDestroyed = true;
    		}
    	}
    }
}
