using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetectCrossing : MonoBehaviour
{
	public Transform target;
	public Transform[] towers;
	public TextMeshPro text;
	public Transform mirrorImage;
	public Transform mirror;
	public SetVector normalArrow;

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

    // Update is called once per frame
    void Update()
    {
		if (towers.Length > 0) {
			// TODO: calculate the normal for reflection

			Vector3 normal = Vector3.zero;


            if (towers.Length == 2) {
				// In 2D, just apply the 2D rotation formula for 90 degrees
				//  on the xz-plane:
				// normal = ... ;

			} else if (towers.Length>=3) {
				// In 3D, the cross product can be used:
				// normal = ... ;
				
				Vector3 dir1 = towers[1].position - towers[0].position;
				Vector3 dir2 = towers[2].position - towers[0].position;

				normal = Vector3.Cross(dir1, dir2);
			}

			if (normalArrow!=null) {
				normalArrow.vector = normal.normalized;
			}

			
			// TODO:
			//  Use dot product to calculate the distance between this line / plane and
			//  the target:
			float distance = 10; // fix this

			if (distance < 0) {
				ActivateAlarm();
			} else {
				DeactivateAlarm();
			}
			if (text!=null) {
				text.text = $"Distance:\n{distance:0.00}";
			}

			// TODO:
			//  Apply vector reflection to place a mirror image object of the target
			//  "behind" the mirror:
			if (mirrorImage!=null) {
				Vector3 mirrorPosition = transform.position; // fix this

                mirrorImage.position = mirrorPosition;

				// TODO (challenge):
				//  Also rotate the mirror image object according to the reflection!
			}

			// Visually place the semi-transparent mirror object:
			//  (If you calculate the normal correctly, nothing needs to be changed here)
			if (mirror!=null) {
				Vector3 avgPosition=Vector3.zero;
				foreach (Transform t in towers) {
					avgPosition += t.position;
				}
				avgPosition /= 3;
				mirror.position = avgPosition;
				mirror.forward = -normal; // why does this even work?
			}
		}
    }
}
