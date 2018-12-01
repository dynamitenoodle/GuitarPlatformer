using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.5f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	bool isPlaying = false;
	public bool IsPlaying { get { return isPlaying; } }

	AudioSource source;

	void Update()
	{
		isPlaying = source.isPlaying;
	}

	public void SetSource (AudioSource _source)
	{
		source = _source;
	}

	public void Play()
	{
		source.volume = volume;
		source.pitch = pitch;
		source.clip = clip;
		source.Play();
	}
}
