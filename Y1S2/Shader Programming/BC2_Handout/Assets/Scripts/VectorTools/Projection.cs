using UnityEngine;

// Use this component to visualize the vector projection of vector [Projectee] onto vector [Onto].
// Drag two game objects with a GetVector component into the inspector fields to make it work.
[RequireComponent(typeof(SetVector))]
public class Projection : MonoBehaviour
{
	public GetVector Projectee;
	public GetVector Onto;
	SetVector visualizer;

	private void Start() {
		visualizer=GetComponent<SetVector>();
	}

	void Update()
    {
        if (visualizer!=null) {
			// vector projection:
			visualizer.vector = Vector3.Dot(Projectee.vector,Onto.vector) * Onto.vector;
		}
    }
}
