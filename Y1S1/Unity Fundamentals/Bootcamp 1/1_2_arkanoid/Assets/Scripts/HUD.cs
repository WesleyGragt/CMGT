using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] private Text lives = null;
	[SerializeField] private Text level = null;
	[SerializeField] private Text score = null;

	public void SetLives(int pLives)
	{
		lives.text = "Lives: " + pLives;
	}

	public void SetScore(int pScore)
	{
		score.text = "Score: " + pScore.ToString().PadLeft(5, '0');
	}

	public void SetLevel (int pLevel, int pMaxLevel)
	{
		level.text = $"Level: {pLevel} / {pMaxLevel}";
	}
}
