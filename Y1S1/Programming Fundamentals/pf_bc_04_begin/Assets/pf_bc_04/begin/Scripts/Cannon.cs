using UnityEngine;

namespace pf_bc_04_begin
{
    // The Cannon class is responsible for shooting bullets in the game,
    // this is the same script as used in bootcamp 3
    public class Cannon : MonoBehaviour
    {
        public Rigidbody bulletPrefab;
        public KeyCode fireKey = KeyCode.Space;
        [Space]
        public float bulletSpeed = 100;
        public float shootCooldown = 1;
        public float timeToLive = 5;

        private bool canShoot = true;
        private float shootTimer = 0;

        private void Update()
        {
            if (canShoot && (Input.GetKey (fireKey) || fireKey == KeyCode.None))
            {
                Rigidbody r = Instantiate(bulletPrefab, transform.position, transform.rotation);
                r.linearVelocity = transform.forward * bulletSpeed;
                Destroy(r.gameObject, timeToLive);
                canShoot = false;
            }

            if (!canShoot)
            {
                shootTimer += Time.deltaTime;

                if (shootTimer >= shootCooldown || Input.GetKeyUp(fireKey))
                {
                    shootTimer = 0.0f;
                    canShoot = true;
                }
            }
        }
    }
}