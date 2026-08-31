using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
	
	//gameplay settings
	[Header("Game play")]
    public int lives = 3;
    public float respawnDelay = 1f;
    public float restartGameDelay = 1f;
	public int pointsPerBrick = 10;

	[Header("Prefabs")]
	public Transform[] levelPrefabs;
    public Paddle paddlePrefab;
    public Ball ballPrefab;
    public Transform paddleSpawnPoint;
	public Transform ballSpawnPoint;

	//screen references
	[Header("UI")]
	public TitleScreen titleScreen;
	public HUD hud;
	public float shakeStrength = 0.2f;

	//audio references
	[Header("Audio")]
	public AudioClip bounceAudio;
	public AudioClip loseAudio;
	public AudioClip respawnAudio;
	public AudioClip brickAudio;
	private AudioSource audioSource;

    //private variables
    private Paddle paddle;
	private Ball ball;
    private Transform currentLevel;
	private int score = 0;
	private Vector3 cameraPosition;
	private int currentLevelIndex = 0;

    void Start()
    {
        Instance = this;
		Application.targetFrameRate = 60;
		cameraPosition = Camera.main.transform.position;
		audioSource = GetComponent<AudioSource>();
        SetupGame();
    }

    private void SetupGame()
    {
		SetupLevel();

		hud.SetLives(lives);
		hud.SetScore(score);

		titleScreen.title.text = "Fire to start !";
		titleScreen.gameObject.SetActive(true);
		hud.gameObject.SetActive(false);
	}

	private void SetupLevel()
	{
		if (currentLevel != null)
		{
			Destroy(currentLevel.gameObject);
		}

		currentLevel = Instantiate(levelPrefabs[currentLevelIndex], transform.position, Quaternion.identity);
		hud.SetLevel(currentLevelIndex + 1, levelPrefabs.Length);

		SetupPaddle();
	}

	private void SetupPaddle()
	{
		if (paddle != null)
		{
			Destroy(paddle.gameObject);
		}

		paddle = Instantiate(paddlePrefab, paddleSpawnPoint.position, Quaternion.identity);
		playSound(respawnAudio);

		SetupBall();
	}

	private void SetupBall()
	{
		if (ball != null)
		{
			Destroy(ball.gameObject);
		}

		ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
		ball.transform.parent = paddle.transform;
	}

	private void ReloadGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1.0f;
	}

	/*********************** EVENT HANDLERS *************************/

	public void OnBoundaryHit()
	{
		playSound(bounceAudio);
	}

	//should be called by ball when the game has started
	public void OnGameStarted()
	{
		titleScreen.gameObject.SetActive(false);
		hud.gameObject.SetActive(true);
	}

	public void LoseLife()
    {
        lives--;
		hud.SetLives(lives);
		playSound(loseAudio);

		//Destroy(gameObject);
		//Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
		//replaced by
		paddle.Explode();
        
        if (lives > 0)
        {
            Invoke(nameof(SetupPaddle), respawnDelay);
        } 
        else 
        {
			titleScreen.title.text = "Game over !";
            titleScreen.gameObject.SetActive(true);
            Time.timeScale = .25f;
			playSound(loseAudio);
			Invoke(nameof(ReloadGame), restartGameDelay);
        }
    }

    public void OnBrickDestroyed() {
		//Debug.Log("OnBrickDestroyed");
		score += pointsPerBrick * lives;
		hud.SetScore(score);
		playSound(brickAudio);

		//camera shake
		Vector3 shakePos = Random.onUnitSphere;
		shakePos *= shakeStrength;
		shakePos.z = 0;
		Camera.main.transform.position += shakePos;

		//Debug.Log(currentLevel.childCount);

		if (currentLevel.childCount < 1)
        {
			currentLevelIndex++;

			Debug.Log(currentLevelIndex + " / " + levelPrefabs.Length);

			if (currentLevelIndex >= levelPrefabs.Length)
			{
				titleScreen.title.text = "You won !";
				titleScreen.gameObject.SetActive(true);
				Time.timeScale = .25f;
				Invoke(nameof(ReloadGame), restartGameDelay);
			} else
			{
				SetupLevel();
			}
		}
    }


	private void playSound(AudioClip pAudio)
	{
		audioSource.PlayOneShot(pAudio);
		//AudioSource.PlayClipAtPoint(pAudio, Camera.main.transform.position);
	}

}
