using UnityEngine;

public class PhysicsBasics_AddForce_Final : MonoBehaviour
{
    public float movementForce = 5;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Next change:
    // to remove the frame rate indepence, we need to use FixedUpdate for all Physics operations.
    //
    // So instead of:
    // private void Update() 
    // we use:
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * movementForce * Input.GetAxis("Vertical"), ForceMode.Force);
    }
}


