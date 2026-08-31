using UnityEngine;
using UnityEngine.SceneManagement;

namespace pf_bc_03_begin
{
    public class SceneReloader : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!gameObject.CompareTag(collision.gameObject.tag))
            {
                if (collision.gameObject.tag == "Player")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                } else if (collision.gameObject.tag == "Obstacle") Destroy(collision.gameObject);
            }
        }
    }
}
