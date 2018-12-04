using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	Sound[] sounds;

	// Use this for initialization
	void Start ()
	{
		foreach (Sound s in sounds)
		{
			s.SetSource(gameObject.AddComponent<AudioSource>());
		}
		PlaySound("Music");
	}

	// Update is called once per frame
	void Update () {
	}

	public void PlaySound(string soundName)
	{
		bool foundSound = false;
		foreach (Sound s in sounds)
		{
			if (s.name == soundName)
			{
				foundSound = true;
				if (!s.IsPlaying)
					s.Play();
				else
					Debug.Log(soundName + " is already playing");
			}
		}
		if (!foundSound)
		{
			Debug.Log("No sound found for name " + soundName);
		}
	}
}
