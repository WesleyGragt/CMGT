using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CreateStairs : MonoBehaviour
{
	public int numberOfSteps = 10;
	// The dimensions of a single step of the staircase:
	public float width = 3;
	public float height = 1;
	public float depth = 1;
	public float centerScalar = 1;
	[Space]
	public float rotateStrength;


	MeshBuilder builder;
	private Vector3 rotatePoint;

	bool started = false;
	private float curStrength;

	void Start()
	{
		builder = new MeshBuilder();
		CreateShape();
		GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
		started = true;
	}

	private void OnValidate()
	{
        if (started)
        {
			curStrength = 0;

            CreateShape();
            GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
            Debug.Log("Recreating stairs");
        }
    }

    void CreateShape()
	{
		builder.Clear();
		
		if (numberOfSteps < 0) numberOfSteps = 0;

        rotatePoint = new Vector3(width * centerScalar, 0, 0);


		for (int i = 0; i < numberOfSteps; i++)
		{
			Vector3 offset = new Vector3(0, height * i, 0 * depth * i);

			// Front:
			int v1 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, 0, 0)), new Vector2(width, 0));
			int v2 = builder.AddVertex(offset + RotateV3(new Vector3(-width / 2, 0, 0)), new Vector2(0, 0));

			int v3 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, height, 0)), new Vector2(width, height));
			int v4 = builder.AddVertex(offset + RotateV3(new Vector3(-width / 2, height, 0)), new Vector2(0, height));

			// Side:
			int s1 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, 0, 0)), new Vector2(0, 0));
			int s3 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, height, 0)), new Vector2(0, height));

			// Top:
			int t3 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, height, 0)), new Vector2(width, 0));
			int t4 = builder.AddVertex(offset + RotateV3(new Vector3(-width / 2, height, 0)), new Vector2(0, 0));

			float outerRadius = rotatePoint.x + width / 2;
			float innerRadius = rotatePoint.x - width / 2;
			float angleDiffRadians = rotateStrength * Mathf.PI / 180;
			curStrength += rotateStrength;
			
			int v5 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, height, depth)), new Vector2(width, innerRadius * angleDiffRadians)); //, RotatePoint(1, 1)); 
			int v6 = builder.AddVertex(offset + RotateV3(new Vector3(-width / 2, height, depth)), new Vector2(0, outerRadius * angleDiffRadians)); // RotatePoint(0, 1));

			// Side:
			int v7 = builder.AddVertex(offset + RotateV3(new Vector3(width / 2, height, depth)), new Vector2(innerRadius * angleDiffRadians, height));
			int v8 = builder.AddVertex(offset + RotateV3(new Vector3(-width / 2, height, depth)), new Vector2(outerRadius * angleDiffRadians, height));

			// Front face:
			builder.AddTriangle(v1, v2, v3, 0, true);
			builder.AddTriangle(v2, v4, v3, 0, true);

			// Top face
			builder.AddTriangle(t3, t4, v5, 0, true);
			builder.AddTriangle(t4, v6, v5, 0, true);

			// Side face:
			builder.AddTriangle(s1, s3, v7, 0);
			builder.AddTriangle(v2, v8, v4, 0);

			// Back face:
			builder.AddTriangle(v1, v5, v6, 0, true);
			builder.AddTriangle(v1, v6, v2, 0, true);
		}
	}

	Vector3 RotateV3(Vector3 pos)
	{
		var point = new Vector3(pos.x, 0, pos.z) - rotatePoint;
		float a = -curStrength * (Mathf.PI / 180);

		float xP = (point.x * Mathf.Cos(a)) - (point.z * Mathf.Sin(a)) + rotatePoint.x;
		float yP = (point.x * Mathf.Sin(a)) + (point.z * Mathf.Cos(a)) + rotatePoint.z;
		// [cos(a), 0, -sin(a)] x   [cos(a)*x 
		// [0, 1, 0]	  y = [
		// [sin(a), 0, cos(a)] z   [
		return new Vector3(xP, pos.y, yP);
	}
}
