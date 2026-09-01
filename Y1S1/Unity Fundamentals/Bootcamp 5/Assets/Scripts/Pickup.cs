using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityFundamentals
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] GameObject ToDestroy;
        
        void OnTriggerEnter(Collider other)
        {
            Debug.Log ("OnTriggerEnter between " + name + " and "+other.name);
            if (ToDestroy != null)
            {
                Destroy(ToDestroy);
            }
        }
    }
}
