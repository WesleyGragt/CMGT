using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10;
    private float damage = 1;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void BulletStats(float pSpeed, float pDamage)
    {
        speed = pSpeed;
        damage = pDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Projectile") return;
        
        if (other.gameObject == PlayerManager.player)
        {
            PlayerManager.playerManager.TakeDamage(damage);
        } 
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
