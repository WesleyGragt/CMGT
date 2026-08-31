using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float paddleSpeed = 0;
    [SerializeField] private float leeway = 0;
	[SerializeField] private ParticleSystem deathParticlesPrefab = null;

	public float currentPaddleSpeed { get; private set; }
	public float maxPaddleSpeed => paddleSpeed;

	void Update()
    {
        Vector3 newPos = transform.position;
		currentPaddleSpeed = Input.GetAxis("Horizontal") * paddleSpeed;
		newPos.x = Mathf.Clamp(newPos.x + currentPaddleSpeed, -leeway, leeway);
        transform.position = newPos;
    }

	public void Explode()
	{
		Destroy(gameObject);
		Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
	}
}
