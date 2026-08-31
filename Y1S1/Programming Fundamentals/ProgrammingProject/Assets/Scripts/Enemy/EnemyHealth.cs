using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (!GameManager.gameManager.startGame)
        {
            GameManager.gameManager.StartGame();
        }
        
        if (health <= 0) Destroy(gameObject);
    }
}
