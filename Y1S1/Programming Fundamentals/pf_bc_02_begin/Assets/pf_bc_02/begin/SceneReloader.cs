using UnityEngine;
using UnityEngine.SceneManagement;

namespace pf_bc_02_begin
{
    // This script reloads the current scene when a collision is detected.
    // Attach this script to a GameObject in your Unity scene that you want to trigger the reload upon collision.
    public class SceneReloader : MonoBehaviour
    {
        // This method is called automatically by Unity when the GameObject this script is attached to
        // collides with another GameObject IF both objects have a COLLIDER and AT LEAST ONE object has a RIGIDBODY
        private void OnCollisionEnter(Collision collision)
        {
            // This line gets the currently active scene and reloads it.
            // SceneManager.LoadScene is used to load a scene by its build index.
            // SceneManager.GetActiveScene().buildIndex gets the build index of the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
