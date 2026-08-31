using UnityEngine;

//Nothing we haven't seen before in bootcamp 2.
public class StaticMover : MonoBehaviour
{
    public Vector3 range;
    public float speed;

    private Vector3 originalPosition = Vector3.zero;
    
    void Start()
    {
        originalPosition = transform.position;    
    }

    void Update()
    {
        transform.position = originalPosition + (Mathf.PingPong(Time.time * speed, 1) - 0.5f) * range;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log (gameObject.name + " collided with " + collision.gameObject.name);
    }
}
