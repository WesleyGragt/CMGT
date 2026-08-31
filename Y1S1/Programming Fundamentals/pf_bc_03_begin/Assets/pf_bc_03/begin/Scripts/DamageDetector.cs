using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageDetector : MonoBehaviour
{
    [SerializeField] int health = 2;

    private void OnCollisionEnter(Collision other)
    {
        if (!gameObject.CompareTag(other.gameObject.tag))
        {
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);

                health--;
                if (health <= 0)
                { 
                    Destroy(gameObject);
                }
            }
        }
    }
}
