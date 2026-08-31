using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeLevel()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "Finish")
        {
            ChangeLevel();
        } else
        {
            ReloadScene();
        }
    }
}
