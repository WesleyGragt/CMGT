using UnityEngine;

namespace pf_bc_07_end_03_final_challenge
{
    public class Enemy : MonoBehaviour
	{
        private float xSpeed = 0;                       // Updated every frame
        [Range(0f, 100f)]                               // Force positive
        public float ySpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public Vector2 bounds = new Vector2(140,120);   // x indicates -x..x range, y indicates -y..y range

        public enum MovementType { StraightDown, TowardsPlayer, SineWave };
        public enum RotationType { None, TowardsPlayer, TowardsMovement };

        [Space(10)]
        public MovementType movementType = MovementType.StraightDown;
        public RotationType rotationType = RotationType.None;

        [Space(10)]
        public float spinAroundLocalZSpeed = 0;

        [Header("Sine wave movement properties")]
        public float sineWaveSpeed = 1;
        public float sineWaveAmplitude = 30;
        private float originalX = 0;

        private void Awake()
        {
            originalX = transform.position.x;
        }

        private void Update()
        {
            Vector3 direction = DoMovement();
            DoRotation(direction);
            DoSpin();
            DestroyIfOutOfBounds();
        }

        private Vector3 DoMovement()
        {
            Player playerReference = Player.instance;

            //straight down, don't do anything
            if (movementType == MovementType.StraightDown) return MoveStraightDown();
            else if (movementType == MovementType.TowardsPlayer) return MoveTowardsPlayer(playerReference);
            else if (movementType == MovementType.SineWave) return MoveSineWave();

            return Vector3.zero;
        }

        private Vector3 MoveStraightDown()
        {
            Vector3 direction = Vector3.down;
            transform.Translate(direction * ySpeed * Time.deltaTime, Space.World);
            return direction;
        }

        private Vector3 MoveTowardsPlayer(Player pPlayerReference)
        {
            xSpeed = Player.instance != null ? Mathf.Sign(Player.instance.transform.position.x - transform.position.x) * ySpeed : 0;
            Vector3 direction = new Vector3(xSpeed, -ySpeed);
            transform.Translate(direction * Time.deltaTime, Space.World);
            return direction;
        }

        private Vector3 MoveSineWave()
        {
            Vector3 position = transform.position;
            position.x = originalX + sineWaveAmplitude * Mathf.Sin(sineWaveSpeed * Time.realtimeSinceStartup);
            position.y -= ySpeed * Time.deltaTime;
            Vector3 direction = (position - transform.position);
            transform.position = position;
            return direction;
        }

        private void DoRotation(Vector3 pDirection)
        {
            Player playerReference = Player.instance;

            if (playerReference != null && rotationType == RotationType.TowardsPlayer) RotateTowardsPlayer(playerReference);
            else if (rotationType == RotationType.TowardsMovement) RotateTowardsMovement(pDirection);
        }

        private void RotateTowardsPlayer(Player pPlayerReference)
        {
            transform.LookAt(pPlayerReference.transform, transform.up);
        }

        private void RotateTowardsMovement(Vector3 pDirection)
        {
            //Basic version (a bit jittery)
            //transform.LookAt(transform.position + pDirection, transform.up);

            //Really awesome smooth version :)
            Quaternion lookatRotation = Quaternion.LookRotation(pDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookatRotation, Time.deltaTime * 5);
        }

        private void DoSpin()
        {
            transform.Rotate(0, 0, spinAroundLocalZSpeed * Time.deltaTime);
        }

        //Alternatively, split this into DestroyIfOutOfBoundsX and DestroyIfOutOfBoundsY
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