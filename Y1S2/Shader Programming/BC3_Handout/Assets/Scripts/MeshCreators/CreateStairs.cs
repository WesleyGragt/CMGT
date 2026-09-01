using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Handout {
	public class CreateStairs : MonoBehaviour {
		public int numberOfSteps = 10;
		// The dimensions of a single step of the staircase:
		public float width=3;
		public float height=1;
		public float depth=1;

		MeshBuilder builder;

		bool started = false;

		void Start () {
			builder = new MeshBuilder ();
			CreateShape ();
			GetComponent<MeshFilter> ().mesh = builder.CreateMesh (true);
			started = true;
		}

        private void OnValidate()
        {
            if (started)
			{
				CreateShape();
				GetComponent<MeshFilter>().mesh = builder.CreateMesh(true);
				Debug.Log("Recreating stairs");
            }
        }

        /// <summary>
        /// Creates a stairway shape in [builder].
        /// </summary>
        void CreateShape() {
			builder.Clear ();

			/**
			// V1: single step, hard coded:
			// bottom:
			int v1 = builder.AddVertex (new Vector3 (2, 0, 0), new Vector2 (1, 0));	
			int v2 = builder.AddVertex (new Vector3 (-2, 0, 0), new Vector2 (0, 0));
			// top front:
			int v3 = builder.AddVertex (new Vector3 (2, 1, 0), new Vector2 (1, 0.5f));	
			int v4 = builder.AddVertex (new Vector3 (-2, 1, 0), new Vector2 (0, 0.5f));
			// top back:
			int v5 = builder.AddVertex (new Vector3 (2, 1, 1), new Vector2 (0, 1));	
			int v6 = builder.AddVertex (new Vector3 (-2, 1, 1), new Vector2 (1, 1));

			builder.AddTriangle (v1, v2, v3);
			builder.AddTriangle (v2, v3, v4);
			builder.AddTriangle (v3, v4, v5);
			builder.AddTriangle (v4, v6, v5);

			/**/
			// V2, with for loop:
			for (int i = 0; i < numberOfSteps; i++)
			{
				Vector3 offset = new Vector3(0, height * i, depth * i);

				// TODO 1: use the width and height parameters from the inspector to change the step width and height

				// TODO 4: Fix the uvs:
				// bottom:
				int v1 = builder.AddVertex(offset + new Vector3(width / 2, 0, 0), new Vector2(1, 0));
				int v2 = builder.AddVertex(offset + new Vector3(-width / 2, 0, 0), new Vector2(0, 0));
				// top front:
				int v3 = builder.AddVertex(offset + new Vector3(width / 2, height, 0), new Vector2(1, 0.5f));
				int v4 = builder.AddVertex(offset + new Vector3(-width / 2, height, 0), new Vector2(0, 0.5f));
				// top back:
				int v5 = builder.AddVertex(offset + new Vector3(width / 2, height, depth), new Vector2(1, 1));
				int v6 = builder.AddVertex(offset + new Vector3(-width / 2, height, depth), new Vector2(0, 1));
				
				int v7 = builder.AddVertex(offset + new Vector3(width / 2, height, depth), new Vector2(0, 0.5f));
				int v8 = builder.AddVertex(offset + new Vector3(-width / 2, height, depth), new Vector2(1, 0.5f));

				// TODO 2: Fix the winding order (everything clockwise):
				// TODO 3: make the mesh solid by adding left, right and back side.
				// Front face:
				builder.AddTriangle(v1, v2, v3, 0, true);
				builder.AddTriangle(v2, v4, v3, 0, true);

				// Top face:
				builder.AddTriangle(v3, v4, v5, 0, true);
				builder.AddTriangle(v4, v6, v5, 0 , true);

				// Side face:
				builder.AddTriangle(v1, v3, v7, 0);
				builder.AddTriangle(v2, v8, v4, 0);

				// Back face:
				builder.AddTriangle(v1, v5, v6, 0, true);
				builder.AddTriangle(v1, v6, v2, 0, true);


				// TODO 5: Fix the normals by *not* reusing a single vertex in multiple triangles with different normals (solve it by creating more vertices at the same position)
				/**/
			}
		}
	}
}