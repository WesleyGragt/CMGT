using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;


public class TextureCreator : MonoBehaviour {
	public enum PatternType { None, Noise, Line, Checker, Gradient, Mandelbrot };

	public PatternType patternType;

    public Vector2 rotatePoint = new Vector2(0.5f, 0.5f);
    public float rotatedDegrees = 0;
	public float rotateStrength = 3;

	const int SIZE = 1024;

	Texture2D texture = null;
	Color[] cols = null;

	void Start() {
		Renderer rend = GetComponent<Renderer>();
		texture = new Texture2D(SIZE, SIZE, TextureFormat.RGBA32, false);
		rend.material.mainTexture = texture;
		texture.wrapMode = TextureWrapMode.Clamp;

		Draw();
	}


	float nextChangeTime = 0;

	void Update()
	{
		if (Time.time > nextChangeTime)
		{
			nextChangeTime = Time.time + 3;
			patternType = (PatternType)((((int)patternType)+1)%6); // easy though maybe ugly?
			if (patternType == PatternType.None) patternType = PatternType.Noise;
            
			rotatedDegrees = Mathf.PingPong(Time.time, 90) - 45;
            rotateStrength = Mathf.PingPong(Time.time, 10) - 5;

            Draw();	
		}
	}

	Color CalculatePixelColor(float u, float v, PatternType pattern) {
		// normalize:
		u = ((u % 1) + 1) % 1;
        v = ((v % 1) + 1) % 1;
        switch (pattern)
		{
			case PatternType.Noise:				
				return Random.value * Color.white;
			case PatternType.Gradient:
				return new Color(u, v, 0, 1);
			case PatternType.Line:
				float lines = 8;
                var colorLine = Color.white* Mathf.Floor((u % (2 / lines) * lines));
				colorLine.a = 1;
				return colorLine;
            case PatternType.Checker:
                float blocksPerRow = 4;
                float blocksPerCollumn = 4;
				float x = Mathf.Floor((u % (2 / blocksPerRow) * blocksPerRow));
				float y = Mathf.Floor((v % (2 / blocksPerCollumn) * blocksPerCollumn));
                var colorChecker = Color.white * ((x + y) % 2);
				colorChecker.a = 1;
                return colorChecker;
            case PatternType.Mandelbrot:
				return Mandelbrot(3 * (u - 0.75f), 3 * (v - 0.5f));
			default:
				return Color.blue;
		}
	}

	void DrawPattern(Color[] cols, int width, int height, PatternType pattern) {
		for (int index = 0; index < width * height; index++)
		{
            int x = index % width;
			int y = index / height;
            float u = (float)x / (width - 1);
            float v = (float)y / (height - 1);

			var point = new Vector2(u, v) - rotatePoint;
			float length = Mathf.Sqrt(point.x * point.x + point.y * point.y);

			float a = rotatedDegrees * (Mathf.PI / 180) + length * rotateStrength;
			
			float uP = (point.x * Mathf.Cos(a)) - (point.y * Mathf.Sin(a)) + rotatePoint.x;
			float vP = (point.x * Mathf.Sin(a)) + (point.y * Mathf.Cos(a)) + rotatePoint.y;

			cols[index] = CalculatePixelColor(uP, vP, pattern);
		}
	}

	void Draw() {
		if (cols == null) {
			cols = texture.GetPixels();
		}
		DrawPattern(cols, SIZE, SIZE, patternType);

		texture.SetPixels(cols);
		texture.Apply();
	}

	void OnValidate() {
		if (texture == null) return;
		Draw();
	}

	#region Mandelbrot
	// Used for the Mandelbrot fractal:
	const int maxIterations = 30;
	const float escapeLengthSquared = 4;

	Color Mandelbrot(float cReal, float cImaginary) {
		int iteration = 0;

		float zReal = 0;
		float zImaginary = 0;

		while (zReal * zReal + zImaginary * zImaginary < escapeLengthSquared && iteration < maxIterations) {
			// Use Mandelbrot's magic iteration formula: z := z^2 + c 
			// (using complex number multiplication & addition - 
			//   see https://mathbitsnotebook.com/Algebra2/ComplexNumbers/CPArithmeticASM.html)
			float newZr = zReal * zReal - zImaginary * zImaginary + cReal;
			zImaginary = 2 * zReal * zImaginary + cImaginary;
			zReal = newZr;
			iteration++;
		}
		// Return a color value based on the number of iterations that were needed to "escape the circle":
		float grad = 1f * iteration / maxIterations; // between 0 and 1
													 // TODO: use a nicer gradient
		return new Color(grad, grad, grad);
	}
	#endregion
}
