using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShotPlayer : MonoBehaviour
{
	public AudioSource[] audioSources;

	public void PlayAudio (int index)
	{
		audioSources[index].Play();
	}
}
