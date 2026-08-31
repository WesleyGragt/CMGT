using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] float spawnInterval = 1;
    float passedTime;
    [Space]
    [SerializeField] Rigidbody spawnObject;
    [SerializeField] float spawnRange;
    [SerializeField] float speed;
    [SerializeField] float timeAlive = 5;

    void Update()
    {
        if (Time.time > passedTime + spawnInterval)
        {
            if (Random.Range(0, 1f) <= 0.75f)
            {
                Rigidbody cube = Instantiate(
                    spawnObject,
                    transform.position + transform.right * Random.Range(-spawnRange, spawnRange),
                    transform.rotation
                    );

                cube.linearVelocity = transform.forward * speed;
                Destroy(cube.gameObject, timeAlive);
            }

            passedTime = Time.time;
        }
    }
}
