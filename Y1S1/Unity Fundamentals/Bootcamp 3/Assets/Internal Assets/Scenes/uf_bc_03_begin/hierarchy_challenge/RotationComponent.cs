using UnityEngine;

public class RotationComponent : MonoBehaviour
{
	public enum Axis { X, Y, Z }
	public enum SpeedSetting { Fast = 2, Medium = 3, Slow = 6 }

	[Header("Rotation Settings")]
	public Axis rotationAxis = Axis.Y;
	public float rotationRange = 45f; // Degrees peak amplitude
	public bool mirrorRange = false;
	public SpeedSetting speed = SpeedSetting.Medium;

	private float time;
	private Vector3 startRotation;

	private void Awake()
	{
		startRotation = transform.localEulerAngles;
	}

	void Update()
	{
		// Advance time
		time += Time.deltaTime;

		float speedToUse = (mirrorRange ? -1 : 1) * (float)speed;

		// Calculate normalized sine value [-1, 1]
		float sine = Mathf.Sin(time / speedToUse * Mathf.PI * 2);
		float angle = sine * rotationRange;

		// Build rotation vector
		Vector3 localRotation = transform.localEulerAngles;
		switch (rotationAxis)
		{
			case Axis.X: localRotation.x = angle; break;
			case Axis.Y: localRotation.y = angle; break;
			case Axis.Z: localRotation.z = angle; break;
		}

		transform.localEulerAngles = startRotation + localRotation;
	}
}
