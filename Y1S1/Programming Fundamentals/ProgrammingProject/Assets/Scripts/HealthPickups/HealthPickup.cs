using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    public float healthGain;

    public void setHealth(float pHealthGain)
    {
        healthGain = pHealthGain;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == PlayerManager.player.transform)
        {
            PlayerManager.playerManager.GainHealth(healthGain);
            Destroy(gameObject);
        }
    }
}
