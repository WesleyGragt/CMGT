using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    public static Transform spawnPoint;
    public static GameObject player;

    [Space]
    [SerializeField] public Slider healthBar;
    [SerializeField] float playerHealth = 100;

    private void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;

        player = gameObject;
        playerManager = this;
        spawnPoint = transform;

        TakeDamage(playerHealth * 0.1f);
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            healthBar.enabled = true;
        } else healthBar.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, healthBar.maxValue);
        healthBar.value = playerHealth;


        if (playerHealth <= 0)
        {
            Respawn();
            Debug.Log("Player died. Now respawning");
        }

        Debug.Log($"Player took {damage} damage. New health is {playerHealth}");
    }

    public void GainHealth(float health)
    {
        playerHealth += health;
        playerHealth = Mathf.Clamp(playerHealth, 0, healthBar.maxValue);

        healthBar.value = playerHealth;

        Debug.Log($"Player got {health} health. New health is {playerHealth}");
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        playerHealth = healthBar.maxValue;
        healthBar.value = playerHealth;
    }
}
