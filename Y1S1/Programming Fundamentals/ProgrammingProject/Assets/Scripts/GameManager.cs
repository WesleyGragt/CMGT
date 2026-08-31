using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI endText;

    [HideInInspector]
    public string endTime;
    [HideInInspector]
    public float addedTime;
    [HideInInspector]
    public bool startGame = false;

    private float startTime;
    private bool stopGame = false;

    private float time;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (gameManager == null)
        {
            gameManager = this;
        } else Destroy(gameObject);
    }

    private void Update()
    {
        if (stopGame) return;

        if (Time.timeScale > 0)
        {
            timerText.enabled = true;
        } else timerText.enabled = false;

        if (timerText == null)
        {
            PlayerManager.player.GetComponent<TextMeshProUGUI>();
            return;
        }

        Timer();
    }

    public void StartGame()
    {
        startGame = true;
        startTime = Time.timeSinceLevelLoad;
    }

    private void Timer()
    {
        if (startGame)
        {
            time = Time.timeSinceLevelLoad - startTime + addedTime;

            timerText.text = TimeToText(time);
        }
        else
        {
            timerText.text = "0:00";
        }
    }

    private string TimeToText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int millisecs = Mathf.FloorToInt((time - minutes * 60 - seconds) * 10);

        if (seconds < 10)
        {
            return $"{minutes}:0{seconds},{millisecs}";
        }
        else
        {
            return $"{minutes}:{seconds},{millisecs}";
        }
    }

    public void StopGame()
    {
        startGame = false;
    }

    public void EndGame()
    {
        StopGame();

        stopGame = true;
        timerText.enabled = false;

        endTime = timerText.text;
        endText.text += $"{endTime}";
        endText.enabled = true;
        
        Cursor.lockState = CursorLockMode.None;
        SceneHandler.sceneHandler.EndGame();
    }
}
