using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    [SerializeField] float damage;

    private Vector3 curPos;
    private Vector3 prevPos;

    private void Update()
    {
        curPos = transform.position - prevPos;

        if (curPos != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(curPos);
        }

        prevPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Projectile") return;

        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
