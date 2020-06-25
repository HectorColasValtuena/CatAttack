using UnityEngine;

namespace CatAttack.FX
{
	public class AudioOneShotPlayer : MonoBehaviour
	{
		public AudioSource[] audioSources;

		public void PlayAudio (int index)
		{
			audioSources[index].Play();
		}
	}
}