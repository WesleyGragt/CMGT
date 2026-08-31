using UnityEngine;

namespace pf_bc_07_begin
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
