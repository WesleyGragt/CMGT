using UnityEngine;

namespace pf_bc_07_end_03_final_challenge
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
