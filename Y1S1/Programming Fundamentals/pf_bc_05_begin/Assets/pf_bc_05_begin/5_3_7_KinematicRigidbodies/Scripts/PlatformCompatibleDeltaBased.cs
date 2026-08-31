using UnityEngine;

//Example of trying to move along with a moving platform...
//There are other solutions out there (eg parenting and unparenting etc)
[RequireComponent (typeof(Rigidbody))]
public class PlatformCompatibleDeltaBased : MonoBehaviour
{
    public float movementForce = 50;
    public float rotationForce = 50;
    public float jumpForce = 15;

    [Range(0.1f, 1)] public float dampening = 0.99f;

    private Rigidbody rb;

    private bool jumpRequested = false;     //If requested, we want to jump
    private bool isGrounded = false;        //If grounded, we CAN jump
    private GameObject previousGround;
    private Vector3 previousGroundPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // If we are grounded we can jump so we request the jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * movementForce * Input.GetAxis("Vertical"), ForceMode.Force);
        rb.AddTorque(Vector3.up * rotationForce * Input.GetAxis("Horizontal"), ForceMode.Force);

        isGrounded = Physics.Raycast(rb.position, Vector3.down, out RaycastHit hitInfo, 1.1f);
        Debug.DrawRay(rb.position, Vector3.down * 1.1f, Color.red, 0, false);

        if (isGrounded)
        {
            if (previousGround == hitInfo.collider.gameObject)
            {
                Vector3 delta = previousGround.transform.position - previousGroundPosition;
                rb.position += delta;
            }

            previousGround = hitInfo.collider.gameObject;
            previousGroundPosition = previousGround.transform.position;
        }
        else
        {
            previousGround = null;
        }


        if (isGrounded && jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        jumpRequested = false;

        rb.linearVelocity = Vector3.Scale(rb.linearVelocity, new Vector3(dampening, 1, dampening));
    }

}