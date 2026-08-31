    using UnityEngine;

public class Jumping_3_CollisionBasedIsGrounded : MonoBehaviour
{
    public float movementForce = 50;
    public float rotationForce = 50;
    public float jumpForce = 15;

    private Rigidbody rb;

    private bool jumpRequested = false;     //If requested, we want to jump
    private bool isGrounded = false;        //If grounded, we CAN jump

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

        if (isGrounded && jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        
        // Note that we now ALWAYS set jumpRequested back to false to avoid late jumping!
        jumpRequested = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }


    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}