using UnityEngine;

namespace pf_bc_07_end_02_first_enemy_types
{
    //Added static instance field
    public class Player : MonoBehaviour
    {
        public static Player instance;

    	public float speed = 5;					// The speed at which the player moves.
    	public Vector2 xMovementRange;			// x = X min value, y = X max value (yes this is confusing)
    	public Vector2 yMovementRange;			// x = Y min value, y = Y max value (yes this is confusing)
    	public float rotationMultiplier = -10;  // Used for banking

    	private Vector3 originalRotation;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
    	{
    		originalRotation = transform.eulerAngles;
    	}

    	private void Update()
    	{
    		//Calculate movement vector
    		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;

    		//Calculate the new (clamped) position based on the old plus the movement
    		Vector3 newPosition = transform.position + movement;
    		newPosition.x = Mathf.Clamp(newPosition.x, xMovementRange.x, xMovementRange.y);
    		newPosition.y = Mathf.Clamp(newPosition.y, yMovementRange.x, yMovementRange.y);
    		transform.position = newPosition;

    		//Rotate the player over the y axis based on how fast we go left and right
    		transform.eulerAngles = originalRotation + Vector3.up * Input.GetAxis("Horizontal") * rotationMultiplier;
    	}

    	private void OnDrawGizmos()
    	{
    		Gizmos.color = Color.green;
    		//From topleft to top right
    		Gizmos.DrawLine(new Vector3(xMovementRange.x, yMovementRange.x, 0), new Vector3(xMovementRange.y, yMovementRange.x, 0));
    		//From top right to bottom right
    		Gizmos.DrawLine(new Vector3(xMovementRange.y, yMovementRange.x, 0), new Vector3(xMovementRange.y, yMovementRange.y, 0));
    		//From bottom right to bottom left
    		Gizmos.DrawLine(new Vector3(xMovementRange.y, yMovementRange.y, 0), new Vector3(xMovementRange.x, yMovementRange.y, 0));
    		//From bottom left to top right
    		Gizmos.DrawLine(new Vector3(xMovementRange.x, yMovementRange.y, 0), new Vector3(xMovementRange.x, yMovementRange.x, 0));
    	}
    }
}
