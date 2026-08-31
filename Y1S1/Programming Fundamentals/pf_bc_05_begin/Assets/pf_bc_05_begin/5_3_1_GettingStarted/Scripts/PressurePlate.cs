using TMPro;
using UnityEngine;

//Simple pressureplate tied to a specific object
public class PressurePlate : MonoBehaviour
{
    public GameObject objectToDetect;

    public string objectThereText;
    public string objectNotThereText;

    public TMP_Text textfield;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToDetect)
        {
            textfield.text = objectThereText;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToDetect)
        {
            textfield.text = objectNotThereText;
        }
    }

}
