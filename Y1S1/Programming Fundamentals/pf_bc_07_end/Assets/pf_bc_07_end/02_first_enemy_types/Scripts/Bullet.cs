using UnityEngine;

namespace pf_bc_07_end_02_first_enemy_types
{
    // Same
    public class Bullet : MonoBehaviour
    {
    	private void OnCollisionEnter(Collision collision)
    	{
    		Debug.Log("Destroying bullet");
    		Destroy(gameObject);
    	}
    }
}
