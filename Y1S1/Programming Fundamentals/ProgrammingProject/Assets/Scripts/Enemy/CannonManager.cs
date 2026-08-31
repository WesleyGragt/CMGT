using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform cannonBarrel;
    [Space]
    [SerializeField] ShootModes shootMode;
    [SerializeField] float fireRate;
    [SerializeField] int bulletAmount;
    [SerializeField] float reloadTime;
    [SerializeField] float spreadAmount;
    [Space]
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletDamage;

    private Transform player;
    private AudioSource gunShotSound;
    private Vector3 shootPoint;
    private Quaternion shotRotation;

    private float fireCountdown = 0f;
    private float reloadCountdown = 0f;
    private float reloadTimer;

    private int curBullet;

    private void Start()
    {
        curBullet = bulletAmount;
        gunShotSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SeesPlayer())
        {
            GunType();
        } else Reload();

        if (curBullet == 0)
        {
            Reload();
        }
    }
    public bool SeesPlayer()
    {
        player = PlayerManager.player.transform;
        if (player == null) return false;

        if (!Physics.Linecast(cannonBarrel.position, player.position, out RaycastHit hit)) return false;

        if (hit.transform == player) return true;

        return false;
    }

    private void GunType()
    {
        switch (shootMode)
        {
            case ShootModes.Automatic:
                AutomaticShoot();
                break;
            case ShootModes.Shotgun:
                ShotgunShoot();
                break;
        }
    }

    private void SpawnBullet()
    {
        shootPoint = cannonBarrel.position + cannonBarrel.up * (cannonBarrel.localScale.y * .2f);
        getDirection();

        GameObject bullet = Instantiate(bulletPrefab, shootPoint, shotRotation);
        bullet.GetComponent<Bullet>().BulletStats(bulletSpeed, bulletDamage);
    }

    private void getDirection()
    {
        float spreadAngleX = Random.Range(-spreadAmount, spreadAmount);
        float spreadAngleZ = Random.Range(-spreadAmount, spreadAmount);
        Quaternion spreadRotation = Quaternion.Euler(shotRotation.eulerAngles + new Vector3(spreadAngleX, 0, spreadAngleZ));
        shotRotation = spreadRotation;
    }

    private void Reload()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0f)
        {
            curBullet = bulletAmount;
            reloadTimer = reloadTime;
        }
    }

    private void AutomaticShoot()
    {
        if (fireCountdown <= 0f && curBullet != 0)
        {
            shotRotation = cannonBarrel.rotation;
            gunShotSound.Play();
            SpawnBullet();
            fireCountdown = 1f / fireRate;

            curBullet--;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void ShotgunShoot()
    {
        if (reloadCountdown <= 0f)
        {
            gunShotSound.Play();

            shotRotation = cannonBarrel.rotation;
            for (int i = 0; i < bulletAmount; i++)
            {
                SpawnBullet();
            }
            reloadCountdown = reloadTime;
        }
        reloadCountdown -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;
        if (Physics.Linecast(shootPoint, player.position, out RaycastHit hit))
        {
            if (hit.transform == player)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
        }

        Gizmos.DrawLine(shootPoint, player.position);
    }
}

public enum ShootModes
{
    Automatic,
    Shotgun
}