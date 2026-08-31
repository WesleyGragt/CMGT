using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sceneHandler;
    private void Start()
    {
        sceneHandler = this;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }
    
    public void RestartScene()
    {
        GameManager.gameManager.StopGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}