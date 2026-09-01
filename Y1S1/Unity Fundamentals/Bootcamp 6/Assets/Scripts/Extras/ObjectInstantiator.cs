using UnityEngine;

namespace UnityFundamentals
{
    // Instantiates the provided object

    public class ObjectInstantiator : MonoBehaviour
    {
    	[SerializeField] private GameObject prefab;
	
    	public void Instantiate()
    	{
    		Instantiate(prefab, transform.position, transform.rotation);
    	}
    }
}
