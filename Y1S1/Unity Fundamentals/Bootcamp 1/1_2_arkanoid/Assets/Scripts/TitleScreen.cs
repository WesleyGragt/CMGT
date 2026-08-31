using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
	public Text title;
	public float xSpeed;
	public float ySpeed;
	public float xRange;
	public float yRange;

	private void Update()
	{
		title.rectTransform.anchoredPosition3D = new Vector3(
				Mathf.Cos (Time.realtimeSinceStartup * xSpeed) * xRange,
				Mathf.Sin (Time.realtimeSinceStartup * ySpeed) * yRange,
				0
			);
	}
}
