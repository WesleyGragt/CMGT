using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Min(1)]
    public int health = 1;

    private void OnTriggerEnter(Collider other)
    {
        health--;
        if (health <= 0) Destroy(gameObject);

        Debug.Log("Oh no! I made a fucky wucky :C");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAH");
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") Destroy(gameObject);
    }
}
