using UnityEngine;

namespace pf_bc_07_end_02_first_enemy_types
{
    //This version can be configured to move towards the player or not

    public class Enemy_Type_1_MoveTowardsOrNot : MonoBehaviour
	{
        private float xSpeed = 0;                       // Updated every frame
        [Range(0f, 100f)]                               // Force positive
        public float ySpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public Vector2 bounds = new Vector2(140,120);   // x indicates -x..x range, y indicates -y..y range

        public bool moveTowardsPlayer = false;          //BASE CHALLENGE
        public float spinAroundLocalZSpeed = 0;         //EXTRA CHALLENGE

		private void Update()
		{
			Vector3 direction = Vector3.down;

            if (Player.instance != null && moveTowardsPlayer)
			{
                //BASE CHALLENGE
                xSpeed = Mathf.Sign(Player.instance.transform.position.x - transform.position.x) * ySpeed;
            }

            transform.Translate(new Vector3(xSpeed, -ySpeed) * Time.deltaTime, Space.World);

            //EXTRA CHALLENGE
            transform.Rotate(0, 0, spinAroundLocalZSpeed * Time.deltaTime);

            DestroyIfOutOfBounds();
        }

        private void DestroyIfOutOfBounds()
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

    }
}