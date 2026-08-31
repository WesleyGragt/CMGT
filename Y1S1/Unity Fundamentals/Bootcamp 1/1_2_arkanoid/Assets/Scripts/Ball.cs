using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialVelocity = 10f;
	public ParticleSystem deathParticlesPrefab;

	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		rb.interpolation = RigidbodyInterpolation.None;
		rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && transform.parent != null)
        {
            transform.parent = null;
            rb.isKinematic = false;
			rb.interpolation = RigidbodyInterpolation.Interpolate;
			rb.linearVelocity = Vector3.up * initialVelocity;
			//rb.velocity = (Vector3.up + Vector3.left * 0.5f + Vector3.right * Random.Range(0.0f,1.0f)).normalized * initialVelocity;
			GameManager.Instance.OnGameStarted();
        }

		if (!rb.isKinematic) rb.linearVelocity = rb.linearVelocity.normalized * initialVelocity;

		//Debug.Log(rb.velocity + " | " + rb.velocity.magnitude);
    }

	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);

		GameManager.Instance.LoseLife();
	}

	private void OnCollisionEnter(Collision collision)
	{
		//if (collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Player"))
		//{
		//	GameManager.Instance.OnBoundaryHit();
		//}

		if (collision.gameObject.CompareTag("Boundary"))
		{
			GameManager.Instance.OnBoundaryHit();
		}


		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.Instance.OnBoundaryHit();

			float deltaX = transform.position.x - collision.transform.position.x;
			if (Mathf.Abs(deltaX) > 4) return;

			Paddle paddle = collision.transform.root.GetComponent<Paddle>();
			float multiplier = paddle.currentPaddleSpeed / paddle.maxPaddleSpeed;
			//rb.velocity = new Vector3(deltaX / 4, 1, 0).normalized * initialVelocity;
			rb.linearVelocity = new Vector3(multiplier, 1, 0).normalized * initialVelocity;
		}

		//Debug.Log(rb.velocity.normalized);
		Vector3 velocity = rb.linearVelocity.normalized;
		if (Mathf.Abs(velocity.y) <= 0.1f)
		{
			//Debug.Log("Correcting");
			velocity.y = Mathf.Sign(velocity.y) * 0.2f;

			if (velocity.y == 0)
			{
				velocity.y = 0.2f;
			}

			velocity.x = Mathf.Sqrt(1 - (velocity.y * velocity.y)) * Mathf.Sign(velocity.x);
			rb.linearVelocity = velocity.normalized * initialVelocity;
		}



	}

}
