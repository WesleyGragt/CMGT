using System.Collections.Generic;
using UnityEngine;

namespace pf_bc_09_begin
{
    public class EnemySpawner : MonoBehaviour
    {
        public Vector2 spawnRangeTopLeft;					    // Spawn range TL in world space
    	public Vector2 spawnRangeBottomRight;				    // Spawn range BR in world space
    	//public int currentWaveSize = 3;
        //public int increasePerWave = 0;                         // For testing
	
    	//public BaseEnemy[] enemyPrefabs;                            // The enemy prefabs to be instantiated.
    	public List<BaseEnemy> lastSpawnedEnemies = new();          // The created enemy instances, public so you can see what happens!

        [System.Serializable]
        public class Wave
        {
            public BaseEnemy[] enemies;
            public int enemyCount;
        }
        public Wave[] waves;

        private int waveCount = 0;
        private BaseEnemy[] enemyPrefabs;

        private void Start()
        {
            enemyPrefabs = waves[0].enemies;
        }

        private void Update()
        {
    		SpawnNewWaveIfAllEnemiesAreDead();
        }

        private void SpawnNewWaveIfAllEnemiesAreDead()
        {
            for (int i = 0; i < lastSpawnedEnemies.Count; i++)
            {
                if (lastSpawnedEnemies[i] != null) return;
            }

    		// If we get here, all enemies are null
    		SpawnEnemies();
        }

        private void SpawnEnemies()
    	{
    		// Clear the list before you add new ones!!
    		lastSpawnedEnemies.Clear();

    		for (int i = 0; i < waves[waveCount].enemyCount; i++)
    		{
    			SpawnEnemy();
    		}

            waveCount++;
            if (waveCount >= waves.Length) waveCount = 0;
        }


        private void SpawnEnemy()
        {
            // Generate a random position within the defined spawn area:
            Vector3 spawnPosition = new Vector3
                (
                    Random.Range(spawnRangeTopLeft.x, spawnRangeBottomRight.x),
                    Random.Range(spawnRangeTopLeft.y, spawnRangeBottomRight.y),
                    0
                );

            // Instantiate an enemy at the random position with the same rotation as the spawner:
            BaseEnemy randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            BaseEnemy enemyInstance = Instantiate(randomEnemyPrefab, spawnPosition, randomEnemyPrefab.transform.rotation);

            lastSpawnedEnemies.Add(enemyInstance);
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
}
