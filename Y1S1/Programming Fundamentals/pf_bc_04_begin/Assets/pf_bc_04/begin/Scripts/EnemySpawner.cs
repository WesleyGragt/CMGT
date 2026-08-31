using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    [Range(0f, 130f)] public float offsetRange;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(0, 70, 0);
        GameObject enemy = Instantiate(enemyPrefab, pos, transform.rotation);
;
        enemy.GetComponent<Enemy>().range.x = offsetRange;
    }
}
