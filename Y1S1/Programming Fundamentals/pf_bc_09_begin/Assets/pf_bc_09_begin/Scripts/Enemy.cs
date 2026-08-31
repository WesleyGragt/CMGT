using UnityEngine;

namespace pf_bc_09_begin
{
    public class Enemy : BaseEnemy
	{
        private float xSpeed = 0;                       // Updated every frame
        [Range(0f, 100f)]                               // Force positive
        public float ySpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public enum MovementType { StraightDown, TowardsPlayer, SineWave };
        public enum RotationType { None, TowardsPlayer, TowardsMovement };

        [Space(10)]
        public MovementType movementType = MovementType.StraightDown;
        public RotationType rotationType = RotationType.None;

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

            DestroyIfOutOfBounds();
            DoSpin();
        }

        private Vector3 DoMovement()
        {
            if (movementType == MovementType.StraightDown) return MoveStraightDown();
            else if (movementType == MovementType.TowardsPlayer) return MoveTowardsPlayer();
            else if (movementType == MovementType.SineWave) return MoveSineWave();

            return Vector3.zero;
        }

        private Vector3 MoveStraightDown()
        {
            Vector3 direction = Vector3.down;
            transform.Translate(direction * ySpeed * Time.deltaTime, Space.World);
            return direction;
        }

        private Vector3 MoveTowardsPlayer()
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
            if (rotationType == RotationType.TowardsPlayer) RotateTowardsPlayer();
            else if (rotationType == RotationType.TowardsMovement) RotateTowardsMovement(pDirection);
        }

        private void RotateTowardsPlayer()
        {
            if (Player.instance != null) transform.LookAt(Player.instance.transform, transform.up);
        }

        private void RotateTowardsMovement(Vector3 pDirection)
        {
            //Basic version (a bit jittery)
            //transform.LookAt(transform.position + pDirection, transform.up);

            //Really awesome smooth version :)
            Quaternion lookatRotation = Quaternion.LookRotation(pDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookatRotation, Time.deltaTime * 5);
        }
    }
}