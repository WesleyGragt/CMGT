using UnityEngine;

namespace pf_bc_09_begin
{
    public class StrafingEnemy : BaseEnemy
    {
        [Range(0f, 100f)]                               // Force positive
        public float movementSpeed = 1.0f;                     // The speed along along the negative world y-axis.

        public bool repeatable = true;
        public StrafingInstruction[] instructions;

        private Vector3 direction;
        private float startTime;
        private int currentInstructionIndex = 0;
        private bool finishedInstructions = false;

        private void Start()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            DestroyIfOutOfBounds();
            DoSpin();

            Movement();
        }

        private void Movement()
        {
            int duration = 0;

            if (instructions.Length == 0 || finishedInstructions)
            {
                direction = Vector3.forward;
                transform.Translate(direction * movementSpeed * Time.deltaTime, Space.Self);
                return;
            }
            else
            {
                GetVector(instructions[currentInstructionIndex].direction);
                duration = instructions[currentInstructionIndex].duration;
            }

            if (startTime + duration >= Time.time)
            {
                transform.Translate(direction * movementSpeed * Time.deltaTime, Space.Self);
            } else
            {
                currentInstructionIndex++;
                startTime = Time.time;
            }

            if (currentInstructionIndex >= instructions.Length)
            {
                if (!repeatable)
                {
                    finishedInstructions = true;
                }
                else
                {
                    currentInstructionIndex = 0;
                    startTime = Time.time;
                }
            }
        }

        private void GetVector(SteeringDirection pDirection)
        {
            switch (pDirection)
            {
                case SteeringDirection.Up:
                    direction = Vector3.back;
                    break;
                case SteeringDirection.Down:
                    direction = Vector3.forward;
                    break;
                case SteeringDirection.Left:
                    direction = Vector3.right;
                    break;
                case SteeringDirection.Right:
                    direction = Vector3.left;
                    break;
            }
        }
    }
}
