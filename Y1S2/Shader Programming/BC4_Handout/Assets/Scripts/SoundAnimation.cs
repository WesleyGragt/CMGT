using UnityEngine;

public class SoundAnimation : MonoBehaviour
{
    public Material texture;
    AudioAnalyzer audioAnalyzer;

    public float decayRand = 0;
    private float transRandX = 0;
    private float transRandY = 0;

    private void Start()
    {
        audioAnalyzer = GetComponent<AudioAnalyzer>();
    }

    private void Update()
    {
        if (audioAnalyzer.IsBeatFrame)
        {
            decayRand = Random.Range(-10f, 10);
            transRandX = Random.Range(-.02f, .02f);
            transRandY = Random.Range(-.02f, .02f);
        }

        texture.SetVector("_Translate", new Vector4(transRandX, transRandY, 0, 0));
        texture.SetFloat("_Decay", decayRand);
    }
}
