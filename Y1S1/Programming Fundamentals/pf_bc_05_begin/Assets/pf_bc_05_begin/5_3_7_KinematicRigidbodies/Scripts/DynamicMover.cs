using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class DynamicMover : MonoBehaviour
{
    public Vector3 range;
    public float speed;
    public bool setDirectly = false;

    private Vector3 originalPosition = Vector3.zero;
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;    

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = originalPosition + (Mathf.PingPong(Time.time * speed, 1) - 0.5f) * range;

        if (setDirectly)
        {
            rb.position = newPosition;
        }
        else
        {
            rb.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log (gameObject.name + " collided with " + collision.gameObject.name);
    }
}
