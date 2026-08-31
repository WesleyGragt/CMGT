using Unity.VisualScripting;
using UnityEngine;

namespace pf_bc_04_begin
{
    public class Player : MonoBehaviour
    {
        public float speed = 5;
        public float borderX = 130;
        public float borderY = 75;

        private void Update()
        {
            transform.Translate(
                speed * Time.deltaTime *
                new Vector3(
                    Input.GetAxis("Horizontal"),
                    -Input.GetAxis("Vertical"),
                    0
                )
            );

            var pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -borderX + 10, borderX - 10);
            pos.y = Mathf.Clamp(transform.position.y, -borderY + 10, borderY - 10);
            transform.position = pos;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(borderX*2, borderY*2));
        }
    }
}