using UnityEngine;

public class ImprovedControls_1_Velocity : MonoBehaviour
{
    public float movementSpeed = 5;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Alternatively we can set the velocity directly, but this often causes jittering,
        // especially when using PhysicsMaterials that are slightly "bouncy".
        // Also we basically skip newton's law that says:
        // velocity can only change through the application of force

        // More specifically from the Unity Manual:

        // "In most cases you should not modify the velocity directly,
        // as this can result in unrealistic behaviour - use AddForce instead.
        // Do not set the velocity of an object every physics step,
        // this will lead to unrealistic physics simulation.
        // A typical usage is where you would change the velocity is when
        // jumping in a first person shooter, because you want an immediate change in velocity."
        // https://docs.unity3d.com/ScriptReference/Rigidbody-linearVelocity.html

        rb.linearVelocity = Vector3.forward * movementSpeed * Input.GetAxis("Vertical");
    }
}