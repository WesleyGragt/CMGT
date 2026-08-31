using UnityEngine;

namespace pf_bc_07_begin
{
    public class Damageable : MonoBehaviour
    {
    	[Min(1)]
    	public int health = 1;
	
    	public GameObject explosionPrefab;

        private bool isDestroyed = false;

        private void OnCollisionEnter(Collision collision)
    	{
            if (isDestroyed) return;


            Debug.Log("Ouch, I was hit by "+collision.gameObject.name);

    		--health;

    		if (health <= 0)
    		{
    			Instantiate(explosionPrefab, transform.position, Quaternion.identity);  
    			GameManager.instance.ObjectWasDestroyed(gameObject);

    			Destroy(gameObject);
                isDestroyed = true;
            }
    	}
    }
}
