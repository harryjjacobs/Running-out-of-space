using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
	private static AudioSource audioSource;
	
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	public static void PlaySFX(AudioClip clip, bool randomizePitch=true)
	{
		float pitch = audioSource.pitch;
		if (randomizePitch) audioSource.pitch = pitch + Random.Range(-1f, 1f);
		audioSource.PlayOneShot(clip);
		audioSource.pitch = pitch;
	}
}
