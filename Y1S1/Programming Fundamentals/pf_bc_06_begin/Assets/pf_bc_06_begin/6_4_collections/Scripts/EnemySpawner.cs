using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public float spawnInterval = 5;         // The time interval between consecutive enemy spawns.
	public int groupSize = 3;              // The number of enemies to spawn in each group.
    public Vector2 spawnRangeTopLeft;       // Spawn range TL in worldspace
	public Vector2 spawnRangeBottomRight;   // Spawn range BR in worldspace
	public List<Enemy> enemyPrefabs;        // The list of enemy prefabs to choose from.

    private Enemy enemyPrefab;               // The enemy prefab to be instantiated.
    private Enemy lastSpawnedEnemy = null;
	private List<Enemy> spawnedEnemies = new List<Enemy>();

	private void SpawnEnemy()
	{
		// Generate a random position within the defined spawn area.
		Vector3 spawnPosition = new Vector3
			(
				Random.Range(spawnRangeTopLeft.x, spawnRangeBottomRight.x),
				Random.Range(spawnRangeTopLeft.y, spawnRangeBottomRight.y),
				0
			);

		enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
		// Instantiate an enemy at the random position with the same rotation as the spawner.
		for (int i = 0; i < groupSize; i++)
		{
			lastSpawnedEnemy = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
			spawnedEnemies.Add(lastSpawnedEnemy);
		}
	}

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
	{
		for (int i = 0; i < spawnedEnemies.Count; i++)
		{
			if (spawnedEnemies[i] == null)
			{
				spawnedEnemies.RemoveAt(i);
				break;
			}
		}

		if (spawnedEnemies.Count == 0)
		{
			groupSize++;
			SpawnEnemy();
		}
	}


    // This method is called to draw gizmos in the Unity editor for visualization purposes.
    public void OnDrawGizmos()
	{
		// Option 1
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(new Vector3(spawnRangeTopLeft.x, spawnRangeTopLeft.y, 0), new Vector3(spawnRangeBottomRight.x, spawnRangeTopLeft.y, 0));
		Gizmos.DrawLine(new Vector3(spawnRangeBottomRight.x, spawnRangeTopLeft.y, 0), new Vector3(spawnRangeBottomRight.x, spawnRangeBottomRight.y, 0));
		Gizmos.DrawLine(new Vector3(spawnRangeBottomRight.x, spawnRangeBottomRight.y, 0), new Vector3(spawnRangeTopLeft.x, spawnRangeBottomRight.y, 0));
		Gizmos.DrawLine(new Vector3(spawnRangeTopLeft.x, spawnRangeBottomRight.y, 0), new Vector3(spawnRangeTopLeft.x, spawnRangeTopLeft.y, 0));

		// Option 2
		Gizmos.color = new Color(0,1,1,0.4f);
		Gizmos.DrawCube ((spawnRangeTopLeft + spawnRangeBottomRight) / 2, spawnRangeBottomRight - spawnRangeTopLeft);
	}
}
