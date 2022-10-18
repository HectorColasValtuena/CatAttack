using UnityEngine;

namespace CatAttack.FX
{
	public class ParticleSystemOneShotPlayer : MonoBehaviour
	{
		public ParticleSystem[] particleSystems;

		public void PlayParticleSystem (int index)
		{
			particleSystems[index].Play();
		}
	}
}