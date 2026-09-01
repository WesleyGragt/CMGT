using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this component to visualize the reflection of one input vector against a normal vector.
// Drag two game objects with a GetVector component into the inspector fields to make it work.
[RequireComponent(typeof(SetVector))]
public class Reflect : MonoBehaviour
{
	public GetVector InputVector;
	public GetVector NormalVector;
	public float ReflectionCoefficient = 1;
	SetVector visualizer;

	private void Start() {
		visualizer = GetComponent<SetVector>();
	}

	void Update() {
		if (visualizer != null) {
			// Exercise 4
			// TODO:
			//  Compute the reflection of [InputVector] onto [NormalVector],
			//  and use visualizer.vector to show the result.
		}
	}
}
