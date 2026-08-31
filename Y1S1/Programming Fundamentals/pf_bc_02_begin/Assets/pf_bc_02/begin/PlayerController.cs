using UnityEngine;

namespace pf_bc_02_begin
{
    // Do you understand and can you explain this whole Script from Bootcamp 1?
    // Or even better can you rewrite this whole Script from memory?
    // If not, repeat that exercise until you can!
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5;

        private void Update()
        {
            transform.Translate(
                new Vector3(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
                ) * speed * Time.deltaTime
            );
        }
    }
}
