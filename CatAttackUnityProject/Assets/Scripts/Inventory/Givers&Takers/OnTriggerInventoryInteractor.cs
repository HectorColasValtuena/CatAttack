using UnityEngine;

namespace CatAttack.Inventory
{
	public abstract class OnTriggerInventoryInteractor : MonoBehaviour
	{
	//serialized fields
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void OnTriggerEnter2D (Collider2D other)
		{
			IInventory inventory = other.GetComponent<InventoryComponent>();
			if (inventory == null) { return; }
			this.InteractWithInventory(inventory);
		}
		
	//ENDOF MonoBehaviour

	//abstract methods
		protected abstract void InteractWithInventory (IInventory inventory);
	//ENDOF abstract methods
	}
}