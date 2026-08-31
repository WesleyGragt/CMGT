using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag != "Player") return;
        PlayerManager.playerManager.Respawn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag != "Player") return;
        PlayerManager.playerManager.Respawn();
    }
}
