using System;
using UnityEngine;
using static TextureCreator;

public enum ModificationType {
	Normal, Invert, Circle
};


public class TextureModifier : MonoBehaviour {
	public Texture2D inputTexture;
	[Tooltip("When true, the texture is modified every time an inspector value is changed")]
	public bool autoRecompute = true;
	public ModificationType patternType = 0;
	[Tooltip("When true, alpha values lower than 1 are shown by blending in the alphaColor")]
	public bool showAlpha = true;
	public Color alphaColor;

	public Vector2 rotatePoint = new Vector2(.5f, .5f);
	public float rotatedDegrees = 0f;
	public float rotateStrength = 1f;

    Color[] inputCols;
	Texture2D texture = null;
	Color[] cols = null;

	void Start() {
		CreateTexture();
		Draw();
	}

	void CreateTexture() {
		Debug.Log("(Re)creating texture");
		inputCols = inputTexture.GetPixels();

		Renderer rend = GetComponent<Renderer>();
		texture = new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.RGBA32, false);
		rend.material.mainTexture = texture;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.filterMode = FilterMode.Point;
		
		cols = texture.GetPixels();
	}

    float nextChangeTime = 0;

    void Update()
    {
        if (Time.time > nextChangeTime)
        {
            nextChangeTime = Time.time + 3;
            patternType = (ModificationType)((((int)patternType) + 1) % 3); // easy though maybe ugly?

            rotatedDegrees = Mathf.PingPong(Time.time, 90) - 45;
            rotateStrength = Mathf.PingPong(Time.time, 10) - 5;

            Draw();
        }
    }

    void DrawPattern(Color[] outputColors, Color[] inputColors, int width, int height, ModificationType pattern) {
		for (int index = 0; index < width * height; index++) {
			Color output = new Color(0, 0, 0, 0);

            int x = index % width;
            int y = index / height;
            float u = (float)x / (width - 1);
            float v = (float)y / (height - 1);

            switch (pattern) {
				case ModificationType.Normal:
					output = inputColors[index];
					break;
				case ModificationType.Invert:
					output = Color.white - inputColors[index];
					output.a = 1;
					break;
				case ModificationType.Circle:
					output = Rotation(u, v);
					break;
			}

			if (showAlpha) {
				outputColors[index] =
					output * output.a +
					alphaColor * (1 - output.a);
			} else {
				outputColors[index] = output;
			}
		}
	}

	private Color Rotation(float u, float v)
	{
        var point = new Vector2(u, v) - rotatePoint;
        float length = Mathf.Sqrt(point.x * point.x + point.y * point.y);

        float a = rotatedDegrees * (Mathf.PI / 180) + length * rotateStrength;

        float uP = (point.x * Mathf.Cos(a)) - (point.y * Mathf.Sin(a)) + rotatePoint.x;
        float vP = (point.x * Mathf.Sin(a)) + (point.y * Mathf.Cos(a)) + rotatePoint.y;

        return inputTexture.GetPixelBilinear(uP, vP);
    }

	void Draw() {
		if (texture.width != inputTexture.width || texture.height != inputTexture.height) {
			CreateTexture();
		}
		DrawPattern(cols, inputCols, texture.width, texture.height, patternType);

		texture.SetPixels(cols);
		texture.Apply();
	}

	void OnValidate() {
		if (texture == null || !autoRecompute) return;
		Draw();
	}
}
