using UnityEngine;

namespace UnityFundamentals
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;

        // Start is called before the first frame update
        void Awake()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        }
 
    }
}
