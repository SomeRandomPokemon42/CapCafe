using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public List<AudioClip> MusicList = new();
	private int CurrentTrack = -1;
	private AudioSource source;

	//Crossfade
	private float CrossfadeTime = 0f;
	private bool Faded;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		SwapToTrack(0);
	}
	private void Update()
	{
		if (CrossfadeTime > 0)
		{
			CrossfadeTime -= Time.deltaTime;

			if (CrossfadeTime - 1 > 0)
			{
				source.volume = CrossfadeTime - 1;
			} else
			{
				if (!Faded)
				{
					Faded = true;
					source.Stop();
					source.clip = MusicList[CurrentTrack];
					source.time = 0;
					source.Play();
				}
				source.volume = 1 - (CrossfadeTime - 1);
			}
		}
	}

	public void SwapToTrack(int track)
	{
		if (CurrentTrack == track) return;
		CurrentTrack = track;
		CrossfadeTime = 2f;
	}
}
