using UnityEngine;

namespace pf_bc_07_end_02_first_enemy_types
{
    // This version uses an integer movement type indicator.
    // Integer values like this are also called magic values as discussed in the bootcamp and are not recommended
    // (Fixed in another iteration)
    // Also notice how messy this version is becoming !!

    public class Enemy_Type_4_RotationType : MonoBehaviour
	{
        private float xSpeed = 0;                       // Updated every frame
        [Range(0f, 100f)]                               // Force positive
        public float ySpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public Vector2 bounds = new Vector2(140,120);   // x indicates -x..x range, y indicates -y..y range

        //0 is go straight down
        //1 is go towards player
        //2 is do sinewave

        public int movementType = 0;

        // 0 dont rotate
        // 1 rotate towards player
        // 2 rotate towards movement

        public int rotationType = 0;

        public float spinAroundLocalZSpeed = 0;

        public float sineWaveSpeed = 1;
        public float sineWaveAmplitude = 30;

        private float originalX = 0;

        private void Awake()
        {
            originalX = transform.position.x;
        }

        private void Update()
		{
            //magic needed for rotation type 1
            Vector3 direction = Vector3.down;

            //straight down, don't do anything different
            if (movementType == 0)
            {
                transform.Translate(new Vector3(0, -ySpeed) * Time.deltaTime, Space.World);
            }
            //towards player
            else if (movementType == 1)
            {
                if (Player.instance != null)
                {
                    xSpeed = Mathf.Sign(Player.instance.transform.position.x - transform.position.x) * ySpeed;
                }

                //magic needed for rotation type 1
                direction = new Vector3(xSpeed, -ySpeed);
                transform.Translate(direction * Time.deltaTime, Space.World);
            }
            //sine wave
            else if (movementType == 2)
            {
                Vector3 position = transform.position;
                position.x = originalX + sineWaveAmplitude * Mathf.Sin(sineWaveSpeed * Time.realtimeSinceStartup);
                position.y -= ySpeed * Time.deltaTime;

                //magic needed for rotation type 1
                direction = position - transform.position;

                transform.position = position;
            }

            if (rotationType == 0)
            {
                //don't do anything
            }
            else if (rotationType == 1)
            {
                if (Player.instance != null)
                {
                    transform.LookAt(Player.instance.transform, transform.up);
                }
            }
            else if (rotationType == 2)
            {
                transform.LookAt(transform.position + direction, transform.up);
            }

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