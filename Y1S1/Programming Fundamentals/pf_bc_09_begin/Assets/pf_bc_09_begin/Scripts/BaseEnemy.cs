using UnityEngine;

namespace pf_bc_09_begin
{
    [DisallowMultipleComponent]
    public abstract class BaseEnemy : MonoBehaviour
    {

        public Vector2 bounds = new Vector2(140, 120);   // x indicates -x..x range, y indicates -y..y range
        
        [Space(10)]
        public float spinAroundLocalZSpeed = 0;

        public void DestroyIfOutOfBounds()
        {
            if (
                transform.position.x < -bounds.x || transform.position.x > bounds.x ||
                transform.position.y < -bounds.y || transform.position.y > bounds.y
            )
            {
                Debug.Log("Destroying enemy out of bounds:" + transform.position);
                // Destroy the enemy game object if it is out of bounds.
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(-bounds.x, -bounds.y, 0), new Vector3(bounds.x, -bounds.y, 0));
            Gizmos.DrawLine(new Vector3(bounds.x, -bounds.y, 0), new Vector3(bounds.x, bounds.y, 0));
            Gizmos.DrawLine(new Vector3(bounds.x, bounds.y, 0), new Vector3(-bounds.x, bounds.y, 0));
            Gizmos.DrawLine(new Vector3(-bounds.x, bounds.y, 0), new Vector3(-bounds.x, -bounds.y, 0));
        }

        public void DoSpin()
        {
            transform.Rotate(0, 0, spinAroundLocalZSpeed * Time.deltaTime);
        }

    }
}
