using UnityEngine;

public class PhysicsBasics_1_AddForce : MonoBehaviour
{
    public float movementForce = 5;

    // Note that the rigidbody is not readily accessible like our transform is
    // we need to explicitly get it, either through a public reference
    // or a get component call
    private Rigidbody rb;

    private void Awake()
    {
        // Cache the rigidbody component in the rb variable
        // If possible, always cache and never use GetComponent in a hot path of your code
        // ("hot path" == code that is executed very often such as an Update method)
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Issue 1: When working with physics, we should no longer be modifying the transform directly
        // transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        // transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        // Replaced the transform.Translate with rb.AddForce ...
        // but movement is still relatively much slower or non-existent...
        // So what is going on? (See next sample)
        rb.AddForce(Vector3.forward * movementForce * Time.deltaTime * Input.GetAxis("Vertical"));
    }
}