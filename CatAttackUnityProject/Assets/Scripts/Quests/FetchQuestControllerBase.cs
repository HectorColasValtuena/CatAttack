using UnityEngine;

namespace CatAttack
{
	public abstract class FetchQuestControllerBase : QuestControllerBase
	{
	//serialized fields
		[SerializeField]
		private GameObject dialog; //dialog informing of the item request
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		//when a trigger linked to this object enters contact with anything with an inventory, check its inventory for the required item
		//use physics layers to avoid unwanted triggers
		private void OnTriggerEnter2D (Collider2D other)
		{
			InventoryController inventory = other.GetComponent<InventoryController>();
			if (inventory == null) { return; }
			if (GetQuestItemFromInventory(inventory) == true)
			{
				this.SetCompleted(inventory);
			}
		}
	//ENDOF MonoBehaviour

	//overrides
		protected override void Cleanup ()
		{
			this.dialog.SetActive(false);
		}
	//ENDOF overrides

	//abstract definitions
		protected abstract bool GetQuestItemFromInventory (InventoryController inventory);
	//ENDOF abstract
	}
}