using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
	public enum FilterMode { AllPass, LowPass};

	public float LPFAlpha = 0.9f;
	public FilterMode filterMode;
	[Tooltip("Use values between 0 and 1. Higher values detect more beats.")]
	public float BeatVolumeTreshold = 0.3f;
	[Tooltip("Shows detected beats in inspector, for debug purposes")]
	public int beatCounter = 0; 

	bool beatFrame = false;
	float lastVolume = 0;
	float lastBassVolume = 0;
	float minBassSinceLastBeat = 0;
	float lastBassChange = 0;

	public bool IsBeatFrame {
		get {
			return beatFrame;
		}
	}
	public float LastVolume {
		get {
			return lastVolume;
		}
	}



	float[] lastSampleLPF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
		beatFrame = false;
    }

	// If an AudioSource is playing, attached to the same GameObject as this script,
	//  OnAudioFilterRead will be called from the audio thread(!) whenever a 
	//  buffer of samples is processed. The float array [data] contains the 
	//  audio samples as 0-1 float values, interleaved in case of stereo audio.
	//  channels=1 for mono, and 2 for stereo input.
	void OnAudioFilterRead(float[] data, int channels) {
		// Keep track of the last low-pass filtered sample in each channel:
		if (lastSampleLPF==null || lastSampleLPF.Length!=channels) {
			lastSampleLPF = new float[channels];
		}

		// Calculate current volume using RMS (RootMeanSquare):
		float SumSqr = 0;
		float SumSqrLPF = 0;
		// Loop over all samples in the buffer:
		for (int i=0;i<data.Length;i+=channels) {
			for (int chan=0;chan<channels;chan++) {
				SumSqr += data[i + chan] * data[i + chan];
				lastSampleLPF[chan] = LPFAlpha * lastSampleLPF[chan] + (1 - LPFAlpha) * data[i + chan];
				SumSqrLPF += lastSampleLPF[chan] * lastSampleLPF[chan];

				if (filterMode==FilterMode.LowPass) {
					data[i + chan] = lastSampleLPF[chan];
				}
			}
		}
		// Use RMS to calculate the volume of both the unfiltered and the low pass signals:
		lastVolume = Mathf.Sqrt(SumSqr / data.Length);
		float currentBassVolume = Mathf.Sqrt(SumSqrLPF / data.Length);

		// Basic beat (=peak) detection, based on bass values:
		float BassChange = currentBassVolume - lastBassVolume;
		lastBassVolume = currentBassVolume;
		if (BassChange<0 && lastBassChange>=0 &&   // previous buffer was local peak
			currentBassVolume *BeatVolumeTreshold >= minBassSinceLastBeat) // peak high enough compared to minimum since last beat
		{
			// beat detected:
			beatFrame = true;
			beatCounter++;
			minBassSinceLastBeat = currentBassVolume;
		} else if (currentBassVolume<minBassSinceLastBeat) {
			minBassSinceLastBeat = currentBassVolume;
		}
		lastBassChange = BassChange;
	}
}
