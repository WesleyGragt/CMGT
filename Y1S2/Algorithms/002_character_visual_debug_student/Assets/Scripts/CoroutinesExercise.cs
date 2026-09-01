using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CoroutinesExercise : MonoBehaviour
{
    /*private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Hello World!");
    }*/

    //--------------------------------------------------------------------------------------------
    /*private void Start()
    {
        StartCoroutine(enableLight());
    }

    private IEnumerator enableLight()
    {
        bool enabled = GetComponent<Light>().enabled;

        yield return new WaitForSeconds(1f);
        GetComponent<Light>().enabled = !enabled;

        Debug.Log($"Light is {enabled}");

        StartCoroutine(enableLight());
    }*/

    //--------------------------------------------------------------------------------------------

    [SerializeField] private List<Light> lights = new List<Light>();
    Light redLight;
    Light yellowLight;
    Light greenLight;
    bool next = false;

    private void Start()
    {
        redLight = lights[0];
        yellowLight = lights[1];
        greenLight = lights[2];

        StartCoroutine(trafficLights());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            next = true;
        }
    }

    private IEnumerator trafficLights()
    {
        greenLight.enabled = false;
        redLight.enabled = true;
        yield return new WaitUntil(() => next);
        next = false;
        redLight.enabled = false;
        yellowLight.enabled = true;
        yield return new WaitUntil(() => next);
        next = false;
        yellowLight.enabled = false;
        greenLight.enabled = true;
        yield return new WaitUntil(() => next);
        next = false;
        StartCoroutine(trafficLights());
    }
}
