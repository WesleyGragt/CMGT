using UnityEngine;

namespace pf_bc_03_begin
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5;

        private void Update()
        {
            transform.Translate(
                speed * Time.deltaTime *
                new Vector3(
                    Input.GetAxis("Horizontal"),
                    0,
                    Input.GetAxis("Vertical")
                ) 
            );
        }
    }
}
