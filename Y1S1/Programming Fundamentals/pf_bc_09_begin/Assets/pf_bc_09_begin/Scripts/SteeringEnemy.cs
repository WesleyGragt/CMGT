using UnityEngine;

namespace pf_bc_09_begin
{
    public class SteeringEnemy : BaseEnemy
	{
        [Range(0f, 100f)]                               // Force positive
        public float movementSpeed = 1.0f;                     // The speed along along the negative world y-axis.

        [Space(10)]
        public bool repeatInstructions = true;          // Whether to repeat the steering instructions when they are done.
        public SteeringInstruction[] steeringInstructions; // The array of steering instructions to follow.

        private float startTime;
        private int currentInstruction = 0;

        private void Start()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.Self);

            DestroyIfOutOfBounds();
            DoSpin();

            if (steeringInstructions.Length != 0) Steering();
        }

        private void Steering()
        {
            if (startTime + steeringInstructions[currentInstruction].duration >= Time.time)
            {
                transform.Rotate(Vector3.up, steeringInstructions[currentInstruction].rotationSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                currentInstruction++;
                startTime = Time.time;
            }

            if (currentInstruction >= steeringInstructions.Length)
            {
                currentInstruction = 0;
            }
        }
    }
}