using UnityEngine;

public class Jumping_1_Basics : MonoBehaviour
{
    public float movementForce = 50;
    public float rotationForce = 50;
    public float jumpForce = 15;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * movementForce * Input.GetAxis("Vertical"), ForceMode.Force);
        rb.AddTorque(Vector3.up * rotationForce * Input.GetAxis("Horizontal"), ForceMode.Force);

        // Let's try this.
        // Does it work? Does it always work? Does it never work? Is it unpredictable? 
        // Spoiler alert: Can be all of the above, so yes unpredictable, but why? (See the slides)
        if (Input.GetKeyDown (KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}