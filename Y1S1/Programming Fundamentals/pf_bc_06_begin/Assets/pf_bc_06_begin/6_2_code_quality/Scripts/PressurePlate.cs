using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

//Simple pressureplate tied to a specific object
public class PressurePlate : MonoBehaviour
{
    public GameObject objectTagToDetect;

    public string objectThereText = "Object detected";
    public string objectNotThereText = "No object detected";

    public TMP_Text textfield;

    List<GameObject> onPlate = new List<GameObject>();

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == objectTagToDetect.tag)
        {
            onPlate.Add(other.gameObject);

            SetText();
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == objectTagToDetect.tag)
        {
            onPlate.Remove(other.gameObject);

            SetText();
        }
    }

    void SetText()
    {
        switch (onPlate.Count)
        {
            case >= 1:
                textfield.text = objectThereText;
                break;
            case 0:
                textfield.text = objectNotThereText;
                break;
        }
    }
}
