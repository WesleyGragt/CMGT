using UnityEngine;

namespace pf_bc_07_end_01_testing_everthing
{
    public class Bullet : MonoBehaviour
    {
    	private void OnCollisionEnter(Collision collision)
    	{
    		Debug.Log("Destroying bullet");
    		Destroy(gameObject);
    	}
    }
}
