using UnityEngine;

namespace pf_bc_07_end_03_final_challenge
{
    // Same
    public class Cannon : MonoBehaviour
    {
    	// The Rigidbody of the bullet to be instantiated and shot.
    	public Rigidbody bulletPrefab;
    	// The magnitude of the velocity the bullet will be shot with
    	public float bulletSpeed = 100;
    	// The time interval between consecutive shots.
    	public float shootCooldown = 1;
    	// How long the bullet will live without hitting anything
    	public float timeToLive = 5;

    	// A boolean to check if the cannon can shoot.
    	private bool canShoot = true;
    	// A timer to keep track of the cooldown period.
    	private float shootTimer = 0;

    	//The key to trigger a shot, if the shootKey is set to KeyCode.None, the cannon will shoot automatically
    	public KeyCode shootKey = KeyCode.None;

    	private void Update()
    	{
    		//CanShoot has to be true in combination with (note the &&) EITHER the shootKey being pressed, or the shootKey being None
    		if (canShoot && (Input.GetKey(shootKey) || shootKey == KeyCode.None))
    		{
    			Rigidbody r = Instantiate(bulletPrefab, transform.position, transform.rotation);
    			r.linearVelocity = transform.forward * bulletSpeed;
    			Destroy(r.gameObject, timeToLive);
    			canShoot = false;
    		}

    		// Reset canShoot when we are not shooting (extra challenge)
    		if (shootKey != KeyCode.None && !Input.GetKey(shootKey)) {
    			canShoot = true;
    		}

    		// If the cannon cannot shoot, start the cooldown timer.
    		if (!canShoot)
    		{
    			// Increase the timer by the time passed since the last frame.
    			shootTimer += Time.deltaTime;

    			// Check if the cooldown period has passed.
    			if (shootTimer >= shootCooldown)
    			{
    				// Reset the timer and allow the cannon to shoot again.
    				shootTimer = 0.0f;
    				canShoot = true;
    			}
    		}
    	}

    }
}
