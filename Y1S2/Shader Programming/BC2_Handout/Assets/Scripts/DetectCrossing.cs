using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetectCrossing : MonoBehaviour
{
	public Transform target;
	public Transform[] towers;
	public TextMeshPro text;

	void ActivateAlarm() {
		foreach (Transform t in towers) {
			t.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
		}
	}
	void DeactivateAlarm() {
		foreach (Transform t in towers) {
			t.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
		}
	}


    void Update()
    {
		// Exercise 2
		// TODO:
		//  Use the dot product to compute whether the target has
		//  crossed the line between the first two towers.
		//  If so, active the alarm.
		//  Show the distance from the target to this line on the given UI element [text].
    }
}
