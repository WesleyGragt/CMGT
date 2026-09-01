using System.Collections;
using UnityEngine;

public class QuadChange : MonoBehaviour
{
    public GameObject objectToChange;

    private TextureCreator texCreate;
    private TextureModifier texModif;

    bool createActive = false;
    bool modifActive = false;

    private void Start()
    {
        texCreate = objectToChange.GetComponent<TextureCreator>();
        texModif = objectToChange.GetComponent<TextureModifier>();
        texCreate.enabled = true;
        StartCoroutine(StateChange());
    }

    private IEnumerator StateChange()
    {
        createActive = true;
        texCreate.patternType = TextureCreator.PatternType.Noise;
        SetRotate();
        yield return new WaitForSeconds(2f);
        texCreate.patternType = TextureCreator.PatternType.Line;
        SetRotate();
        yield return new WaitForSeconds(5f);
        texCreate.patternType = TextureCreator.PatternType.Checker;
        SetRotate();
        yield return new WaitForSeconds(5f);
        texCreate.patternType = TextureCreator.PatternType.Gradient;
        SetRotate();
        yield return new WaitForSeconds(5f);
        createActive = false;

        StartCoroutine(StateChange());
    }

    private void SetRotate()
    {
        texCreate.rotatedDegrees = Mathf.PingPong(Time.time, 100) - 50;
        texCreate.rotateStrength = Mathf.PingPong(Time.time, 3) - 1.5f;
    }

    private IEnumerator EnableScript()
    {
        if (createActive)
        {
            texCreate.enabled = false;
            yield return new WaitForSeconds(.1f);
            texCreate.enabled = true;
        } else if (modifActive)
        {
            texModif.enabled = false;
            yield return new WaitForSeconds(.1f);
            texModif.enabled = true;
        }

        StartCoroutine(EnableScript());
    }
}
