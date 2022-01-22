using UnityEngine;

namespace CatAttack.Inventory
{
	public class OnTriggerItemGiver : MonoBehaviour
	{
	//serialized fields
		//list of items given to the player
		[SerializeField]
		private EItemID[] givenItems;

		//this gameObject will be disabled once item is picked up
		[SerializeField]
		private GameObject disableOnPickup;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void OnTriggerEnter2D (Collider2D other)
		{
			InventoryController inventory = other.GetComponent<InventoryController>();
			if (inventory == null) { return; }
			this.GiveItems(inventory);
			this.disableOnPickup?.SetActive(false);
		}
		
		private void Awake ()
		{
			if (this.givenItems.Length == 0)
			{ Debug.LogWarning(string.Format("{0}.OnTriggerItemGiver no items given defined!!", this.gameObject.name)); }
		}
	//ENDOF MonoBehaviour

	//private methods
		private void GiveItems (IInventory inventory)
		{
			foreach (EItemID item in this.givenItems)
			{ inventory.Add(item); }
		}
	//ENDOF private methods
	}
}