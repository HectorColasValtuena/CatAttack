using UnityEngine;

namespace CatAttack
{
	public class StarpowerFullRestocker : MonoBehaviour
	{
		private const string requiredTag = "Player";

		public void OnTriggerEnter2D (Collider2D other)
		{
			if (other.tag == requiredTag)
			{
				other.GetComponent<StarpowerReservoir>().RegenerateStarpower();
			}
		}
	}
}