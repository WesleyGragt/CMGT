using UnityEngine;

public class FireBullet : MonoBehaviour
{
    float passedTime;

    [Space]
    [SerializeField] Rigidbody bulletRB;
    [SerializeField] int maxAmmo = 10;
    int ammo;
    [SerializeField] int speed = 100;
    [SerializeField] float cooldown = 0.5f;

    private void Start()
    {
        ammo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && ammo != 0 && Time.time > passedTime + cooldown)
        {
            Rigidbody r = Instantiate(bulletRB, transform.position + transform.forward*0.5f, transform.rotation);
            r.linearVelocity = transform.forward * speed;
            Destroy(r.gameObject, 3);

            passedTime = Time.time;
            ammo--;
        }
        else if (!Input.GetMouseButton(0)) passedTime = 0;

        if (Input.GetKeyDown(KeyCode.R)) ammo = maxAmmo;
    }
}
