using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModificationType {
	Normal, Blur, Invert, Circle
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
	public float strength = 1f;

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

	void DrawPattern(Color[] outputColors, Color[] inputColors, int width, int height, ModificationType pattern) {
		for (int index = 0; index < width * height; index++) {
			Color output = new Color(0, 0, 0, 0);

            int x = index % width;
            int y = index / height;
            float u = (float)x / (width - 1);
            float v = (float)y / (height - 1);

			u += .3f;
			Color colInput = texture.GetPixelBilinear(u, v);

            switch (pattern) {
				case ModificationType.Normal:
					output = inputColors[index];
					break;
				case ModificationType.Blur:
                    output = Blur(inputColors, x, y, width, height);
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
        float length = Mathf.Sqrt(u * u + v * v);
        float a = rotatedDegrees * (MathF.PI / 180) + length * strength;

        float pointU = u - rotatePoint.x;
        float pointV = v - rotatePoint.y;

        float uP = (pointU * Mathf.Cos(a)) - (pointV * Mathf.Sin(a));
        float vP = (pointU * Mathf.Sin(a)) + (pointV * Mathf.Cos(a));

		float uF = rotatePoint.x + uP;
		float vF = rotatePoint.y + vP;

        return inputTexture.GetPixelBilinear(uF, vF);
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

	private void Update() {
		if (Input.GetKeyDown(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
			var exporter = GetComponent<TextureExporter>();
			if (exporter != null) {
				exporter.ExportTexture(texture);
			}
		}
	}

	#region convolution
	static int[,] blur =
	{
		{1,2,1},
		{2,4,2},
		{1,2,1}
	};

	static Color ApplyConvolution(Color[] inputColors, int x, int y, int width, int height, int[,] convolution, float numerator, bool includeAlpha = false) {
		Color output = new Color(0, 0, 0, 0);
		// Assumes the convolution matrix is a 3x3 matrix:
		for (int dx = 0; dx < 3; dx++) {
			for (int dy = 0; dy < 3; dy++) {
				int inX = x + dx - 1; // From x-1 to x+1
				int inY = y + dy - 1; // From y-1 to y+1
				if (inX >= 0 && inX < width && inY >= 0 && inY < height) {
					output += inputColors[inX + inY * width] * convolution[dx, dy];
				} // out of range is implicitly considered to be black
			}
		}
		output /= numerator;
		if (!includeAlpha) output.a = 1;
		return output;
	}

	static Color Blur(Color[] inputColors, int x, int y, int width, int height) {
		return ApplyConvolution(inputColors, x, y, width, height, blur, 16, true);
	}

	#endregion
}
