using UnityEngine;

public class Jumping_2_CorrectInput : MonoBehaviour
{
    public float movementForce = 50;
    public float rotationForce = 50;
    public float jumpForce = 15;

    private Rigidbody rb;

    private bool jumpRequested = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Correctly poll input in Update
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        // Should we also buffer GetAxis in Update and process it here?
        // Officially it would be better, but the effects of not doing it are less detrimental
        rb.AddRelativeForce(Vector3.forward * movementForce * Input.GetAxis("Vertical"), ForceMode.Force);
        rb.AddTorque(Vector3.up * rotationForce * Input.GetAxis("Horizontal"), ForceMode.Force);

        // Process jump request in FixedUpdate
        // But how many times can we jump?
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpRequested = false;
        }
    }

}