using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementForce = 50;
    [SerializeField] private float RotationForce = 50;
    [SerializeField] float jumpForce = 5;
    [Range(0.1f, 1)] public float d = 0.2f;
    public float pushForce = 5;
    
    private Rigidbody rb;
    private Rigidbody attachedRigidbody = null;
    private bool attachDetachBlockRequested = false;
    
    bool isGrounded = false;                
    private bool jumpRequested = false;             
    const int maxJumpCount = 2;
    private int jumpCount = maxJumpCount;
    
    bool shoveBlockRequested = false;            
    private void Awake()    
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) jumpRequested = true;
        if (Input.GetKeyDown(KeyCode.E)) attachDetachBlockRequested = true;
        if (Input.GetKeyDown(KeyCode.F)) shoveBlockRequested = true;
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();


        jumpRequested = false;

        if (rb.linearVelocity.y <= 0)
        {
            HandleGrounded();
            HandleShoving();
            
            attachDetachBlockRequested = false;
            shoveBlockRequested = false;
        }
    }

    private void HandleMovement()
    {
        rb.AddRelativeForce(Vector3.forward * movementForce * Input.GetAxis("Vertical"), ForceMode.Force);
        rb.AddTorque(Vector3.up * RotationForce * Input.GetAxis("Horizontal"), ForceMode.Force);
        rb.linearVelocity = Vector3.Scale(rb.linearVelocity, new Vector3(d, 1, d));
    }
    
    private void HandleJumping()
    {
        if (!(jumpCount < maxJumpCount && jumpRequested)) return;
            jumpCount++;

            Vector3 vel = rb.linearVelocity;
            vel.y = jumpForce;
            rb.linearVelocity = vel;
    }
    
    private void HandleGrounded()
    {
        isGrounded = Physics.Raycast(rb.position, Vector3.down, 1.1f);
        Debug.DrawRay(rb.position, Vector3.down * 1.1f, Color.red, 0, false);

        if (isGrounded) jumpCount = 0;
        else rb.AddForce(Vector3.down * 9.81f, ForceMode.Force);
    }

    private void HandleShoving()
    {
        if (!(isGrounded && shoveBlockRequested && attachedRigidbody == null)) return;
        if (!(Physics.Raycast(transform.position, transform.forward, out RaycastHit something, 1))) return;
        
        if (something.rigidbody.name == "Obstacle")
        {
            something.rigidbody.AddForce(transform.forward * pushForce, ForceMode.Impulse);
        }

        HandleAttachDetachBlock();
    }

    private void HandleAttachDetachBlock()
    {
        if (!attachDetachBlockRequested) return;

        GetAttachedRigidBody();
        if (attachedRigidbody != null)
        {
            attachedRigidbody.transform.parent = null;
            attachedRigidbody.isKinematic = false;
            attachedRigidbody = null;
        }
        else
        {
            attachedRigidbody = GetAttachedRigidBody();

            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = true;
                attachedRigidbody.transform.parent = transform;
                attachedRigidbody.transform.localPosition = Vector3.forward * 1.5f;
            }
        }
    }


    private Rigidbody GetAttachedRigidBody()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1) && hitInfo.collider.CompareTag("Obstacle"))

            if (!(attachedRigidbody == null)) return attachedRigidbody = hitInfo.rigidbody;
        return null;
    }
}

