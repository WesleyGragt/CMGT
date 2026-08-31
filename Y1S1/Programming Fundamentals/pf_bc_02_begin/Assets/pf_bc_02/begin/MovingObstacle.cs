using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] bool moving = false;
    [SerializeField] bool rotating = false;
    [Space]
    [SerializeField] Vector3 moveRange = new Vector3(1, 0, 0);
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float phaseOffset = 1f;
    [Space]
    [SerializeField] Vector3 rotateRange = new Vector3(1, 0, 0);
    [SerializeField] float rotateSpeed = 1f;

    Vector3 basePos = Vector3.zero;
    Vector3 baseRot = Vector3.zero;

    private void Start()
    {
        basePos = transform.position;
        baseRot = transform.eulerAngles;
    }
    private void Update()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles);
        
        if (moving)
        {
            Vector3 offsetMove = moveRange * Mathf.Sin(Time.time * moveSpeed + phaseOffset * Mathf.PI);
            Vector3 movement = rotation * offsetMove;

            transform.position = basePos + movement;
        }

        if (rotating)
        {
            Vector3 offsetRotate = rotateRange * Mathf.Sin(Time.time * rotateSpeed + phaseOffset * Mathf.PI);
            Vector3 rotate = rotation * offsetRotate;
            
            transform.eulerAngles = baseRot + rotate;
        }
    }
}
