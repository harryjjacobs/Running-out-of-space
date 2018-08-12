using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{

	public AudioSource audioSource;
	public float AudioFadeInDuration = 2f;
	private bool fading = false;

	// Use this for initialization
	void Start ()
	{
		GameController.LevelLoadedEvent += OnLevelLoaded;
		GameController.LevelCompletedEvent += OnLevelCompleted;
	}

    private void OnLevelCompleted(Level level)
    {
        StartCoroutine(FadeOutAudio(AudioFadeInDuration));
    }

    private void OnLevelLoaded(Level level)
    {	
		// If this isn't the first level then
		// find random place to play audio from,
		// and then fade it in
		if (level.Number > 0)
		{
			StartCoroutine(FadeInAudioAtRandomPosition(AudioFadeInDuration));
		}

    }

	IEnumerator FadeInAudioAtRandomPosition(float duration)
	{
		while (fading)
		{
			yield return null;
		}
		
		fading = true;
		audioSource.time = Random.Range(0, audioSource.clip.length);
		audioSource.Play();

		audioSource.volume = 0;

		float resolution = 20;
		for (int i = 0; i < duration*resolution; i++)
		{
			audioSource.volume = (float)i / (duration*resolution);
			yield return new WaitForSeconds(1f/resolution);
		}
		audioSource.volume = 1;
		fading = false;
	}

	IEnumerator FadeInAudio(float duration)
	{
		fading = true;
		audioSource.volume = 0;
		float resolution = 20;
		for (int i = 0; i < duration*resolution; i++)
		{
			audioSource.volume = (float)i / (duration*resolution);
			yield return new WaitForSeconds(1f/resolution);
		}
		audioSource.volume = 1;
		fading = false;
	}

	IEnumerator FadeOutAudio(float duration)
	{
		fading = true;
		audioSource.volume = 1;
		float resolution = 20;
		for (int i = 0; i < duration*resolution; i++)
		{
			audioSource.volume = 1 - (float)i / (duration*resolution);
			yield return new WaitForSeconds(1f/resolution);
		}
		audioSource.volume = 0;
		fading = false;
	}
}
