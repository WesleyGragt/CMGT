using UnityEngine;

namespace pf_bc_09_begin
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
