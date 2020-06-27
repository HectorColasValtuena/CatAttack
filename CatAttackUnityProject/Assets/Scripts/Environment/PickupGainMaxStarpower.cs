using UnityEngine;

namespace CatAttack
{
	public class PickupGainMaxStarpower : MonoBehaviour
	{
		private const string requiredTag = "Player";
		private const int starpowerGained = 1;
		
		void OnTriggerEnter2D (Collider2D other) 
		{
			if (other.tag == requiredTag)
			{
				StarpowerReservoir spReservoir = other.GetComponent<StarpowerReservoir>();
				if (spReservoir == null)
				{
					Debug.LogError("PickupGainMaxStarpower found player object without StarpowerReservoir.");
					return;
				}
				spReservoir.GainMaximumStarpower(starpowerGained);
				gameObject.GetComponent<Animator>().SetBool("Depleted", true);
				Destroy(this);
			}
		}
	}
}