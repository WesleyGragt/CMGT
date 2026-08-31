using UnityEngine;

namespace pf_bc_03_begin
{
    //The smoothing moving sine obstacle from the previous bootcamp! It's back! :)
    public class MovingObstacle : MonoBehaviour
    {
        private Vector3 originalPosition;

        [Range(0,10)] public float movementSpeed = 1;
        [Range(0, 2)] public float phaseOffset = 0;

        public Vector3 movementRange;

        private void Start()
        {
            originalPosition = transform.position;
        }

        private void Update()
        {
            Vector3 positionOffset = 
                transform.TransformVector(movementRange) *
                Mathf.Sin(Time.time * movementSpeed + phaseOffset * Mathf.PI);

            transform.position = originalPosition + positionOffset;
        }
    }
}
