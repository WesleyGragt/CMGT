using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] Transform exitDoor;
    [SerializeField] Transform endPointDoor;
    [SerializeField] float doorSpeed;
    [Space]
    [SerializeField] Transform playerSpawn;

    private AudioSource doorSound;
    private bool enemiesDefeated = false;
    private bool exitAnim = false;
    private bool exitOpen = false;

    private void Start()
    {
        doorSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!enemiesDefeated)
        {
            CheckEnemies();
        }

        if (exitAnim && !exitOpen)
        {
            OpenExit();
        }
    }

    private void CheckEnemies()
    {
        if (enemies.Count == 0)
        {
            StartToOpen();
            return;
        }
        
        for (int i = 0; i < enemies.Count;)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
            else i++;
        }
    }

    public void StartToOpen()
    {
        enemiesDefeated = true;
        exitAnim = true;

        doorSound.Play();

        OpenExit();
    }

    private void OpenExit()
    {
        exitDoor.position = Vector3.Lerp(exitDoor.position, endPointDoor.position, doorSpeed * Time.deltaTime);

        if (Vector3.Distance(exitDoor.position, endPointDoor.position) < 0.1f)
        {
            exitOpen = true;

            if (playerSpawn == null) return;
            PlayerManager.spawnPoint = playerSpawn;
        }
    }
}
