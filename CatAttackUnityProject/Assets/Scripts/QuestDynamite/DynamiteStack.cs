using UnityEngine;

namespace CatAttack
{		
	public class DynamiteStack : MonoBehaviour
	{
		public GameObject disableStick;

		public void OnTriggerEnter2D (Collider2D other)
		{
			InventoryController inventory = other.GetComponent<InventoryController>();
			if (inventory == null) { return; }
			inventory.hasDynamite = true;
			if (disableStick != null) { disableStick.SetActive(false); }
		}
	}
}