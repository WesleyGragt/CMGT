using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace pf_bc_07_end_01_testing_everthing
{
    public class GameManager : MonoBehaviour
    {
    	// Reference to the enemySpawner in the Scene so we can enable it when the game starts
    	public GameObject enemySpawner;
    	// Reference to a GameOverReloadButton that needs to be enabled when the game is over
    	public GameObject gameOverReloadButton;
    	// Prefab so we can respawn the player when it dies
    	public Player playerPrefab;

    	// References to the score and lives textfields
    	public TMP_Text scoreTextField;
    	public TMP_Text livesTextField;

    	private int score = 0;
    	private int lives = 3;

    	public static GameManager instance; //even better to do this! { get; private set; }

    	private void Awake()
    	{
    		instance = this;

    		// To avoid issues, you should also check if there already WAS an instance of this component in the Scene
    		// Skipped for clarity, ask the teacher if interested
    	}


    	//This adds a call to this method to the context menu in the GameManager inspector
    	[ContextMenu("Start game!")]
    	//Make sure this method is public for the UI step!
    	public void StartGame()
    	{
    		enemySpawner.SetActive(true);

    		// Make sure all textfields are updated
    		UpdateScore(score);
    		UpdateLives(lives);
    	}

    	public void ObjectWasDestroyed(GameObject go)
    	{
    		Debug.Log("Object was destroyed: " + go.name);

    		// Enemy destroyed?
    		if (go.GetComponent<Enemy>() != null)
    		{
    			UpdateScore(score + 1);
    		}
    		// Player destroyed?
    		else if (go.GetComponent<Player>() != null)
    		{
    			UpdateLives(lives - 1);

    			if (lives > 0)
    			{
    				// lives left? respawn
    				Instantiate(playerPrefab);
    			}
    			else
    			{
    				// no lives left? show game over button to reload the whole scene
    				gameOverReloadButton.SetActive(true);
                    enemySpawner.SetActive(false);
    			}
    		}
    	}

    	private void UpdateScore(int pScore)
    	{
    		score = pScore;
    		scoreTextField.text = "Score: " + score;
    	}

    	private void UpdateLives(int pLives)
    	{
    		lives = pLives;
    		livesTextField.text = "Lives: " + lives;
    	}

    	public void ReloadScene()
    	{
    		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    	}
    }
}
