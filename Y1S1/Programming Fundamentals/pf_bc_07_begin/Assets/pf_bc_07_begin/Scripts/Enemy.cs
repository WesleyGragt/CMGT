using UnityEngine;
using UnityEngine.TestTools;

namespace pf_bc_07_begin
{
    public class Enemy : MonoBehaviour
    {
        public Vector2 bounds;
        [Space]
        public bool lookAtPlayer = false;
        public bool rotating = false;
        public Vector2 rotationSpeed;
        [Space]
        public bool randomXSpeed = true;
        public bool moveToPlayer = false;
        
        public bool waveX = false;
        public float waveFrequency = 1f;
        public float waveAmplitude = 1f;

        public Vector2 speed;

        Player playerInstance;

        private Vector3 startPos;

        private void Start()
    	{
    		//pick a random xspeed based on the provided yspeed
    		if (randomXSpeed) speed.x = Random.Range(-speed.y, speed.y);

            startPos = transform.position;
        }

    	private void Update()
    	{
            Movement();
            Rotation();
    	}

    	private void OnDrawGizmos()
    	{
    		Gizmos.color = Color.red;
    		Gizmos.DrawLine(new Vector3(-bounds.x, -bounds.y, 0), new Vector3(bounds.x, -bounds.y, 0));
    		Gizmos.DrawLine(new Vector3(bounds.x, -bounds.y, 0), new Vector3(bounds.x, bounds.y, 0));
    		Gizmos.DrawLine(new Vector3(bounds.x, bounds.y, 0), new Vector3(-bounds.x, bounds.y, 0));
    		Gizmos.DrawLine(new Vector3(-bounds.x, bounds.y, 0), new Vector3(-bounds.x, -bounds.y, 0));
    	}

        private void Movement()
        {
            if (playerInstance != null && moveToPlayer)
            {
                float playerX = playerInstance.transform.position.x;

                if (transform.position.x < playerX)
                {
                    speed.x = Mathf.Abs(speed.x);
                }
                else
                {
                    speed.x = -Mathf.Abs(speed.x);
                }
            }

            //Y = A * sin(L * PI) + U/D

            transform.Translate(new Vector3(speed.x, -speed.y) * Time.deltaTime, Space.World);

            // Check if the enemy has moved out of the game bounds.
            if (
                transform.position.x < -bounds.x || transform.position.x > bounds.x ||
                transform.position.y < -bounds.y || transform.position.y > bounds.y
                )
            {
                Debug.Log("Destroying enemy out of bounds:" + transform.position);
                Destroy(gameObject);
            }
        }

        private void Rotation()
        {
            playerInstance = Player.instance;

            if (rotating && playerInstance != null) transform.Rotate(0, 0, rotationSpeed.y * Time.deltaTime);
            if (lookAtPlayer && playerInstance != null) transform.LookAt(playerInstance.transform.position, transform.up);
        }

    }
}
