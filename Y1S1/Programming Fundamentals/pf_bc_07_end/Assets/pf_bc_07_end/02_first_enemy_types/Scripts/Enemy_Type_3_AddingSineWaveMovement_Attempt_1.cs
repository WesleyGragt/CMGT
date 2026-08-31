using UnityEngine;

namespace pf_bc_07_end_02_first_enemy_types
{
    // This class demonstrates how boolean flags are not the right approach for multiple mutually exclusive behaviours,
    // since multiple flags might indicate/request conflicting behaviours.
    // We should use SOME kind of movement TYPE instead (see next iteration)

    public class Enemy_Type_3_AddingSineWaveMovement_Attempt_1 : MonoBehaviour
	{
        private float xSpeed = 0;                       // Updated every frame
        [Range(0f, 100f)]                               // Force positive
        public float ySpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public Vector2 bounds = new Vector2(140,120);   // x indicates -x..x range, y indicates -y..y range

        public bool moveTowardsPlayer = false;          
        public bool rotateTowardsPlayer = false;        // BASE CHALLENGE
        public float spinAroundLocalZSpeed = 0;


        // You might think about doing something like this:

        public bool sineWaveMovement = false;
        public float sineWaveSpeed = 1;
        public float sineWaveAmplitude = 30;

        // AND THEN WHAT? (In other words, this hasn't been implemented, since how would we decide in a logical way
        // which behaviour would get precedence if both moveTowardsPlayer and sineWaveMovement are true?
        // This just leads to confusing situations where users would have to understand the full source in order to 
        // configure an enemy correctly.

        // ...

        private void Update()
		{
			Vector3 direction = Vector3.down;

            if (Player.instance != null)
            {
                if (moveTowardsPlayer)
                {
                    xSpeed = Mathf.Sign(Player.instance.transform.position.x - transform.position.x) * ySpeed;
                }

                if (rotateTowardsPlayer)
                {
                    //BASE CHALLENGE: the lookat that doesn't work with the spin option
                    //transform.LookAt(playerReference.transform);

                    //EXTRA CHALLENGE: the lookat that does work with the spin option
                    transform.LookAt(Player.instance.transform, transform.up);
                }
            }

            transform.Translate(new Vector3(xSpeed, -ySpeed) * Time.deltaTime, Space.World);

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