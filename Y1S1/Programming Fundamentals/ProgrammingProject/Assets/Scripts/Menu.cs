using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    
    private SceneHandler sceneHandler;
    private bool isActive;

    private void Update()
    {
        if (sceneHandler == null)
        {
            sceneHandler = SceneHandler.sceneHandler;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    
    public void TogglePauseMenu()
    {
        isActive = canvas.activeSelf;

        canvas.SetActive(!isActive);

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else Cursor.lockState = CursorLockMode.None;

        if (isActive)
        {
            Time.timeScale = 1f;
        }
        else Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        sceneHandler.RestartScene();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (GameManager.gameManager != null) Destroy(GameManager.gameManager.gameObject);
        sceneHandler.ReturnToMainMenu();
    }
}
