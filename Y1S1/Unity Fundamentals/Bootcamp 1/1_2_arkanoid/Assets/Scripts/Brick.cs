using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brickParticle;
	public GameObject next;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

		if (next != null)
		{
			Instantiate(next, transform.position, transform.rotation, transform.parent);
		}

		gameObject.transform.parent = null;
        Instantiate(brickParticle, transform.position, Quaternion.identity);
        GameManager.Instance.OnBrickDestroyed();
    }
}
