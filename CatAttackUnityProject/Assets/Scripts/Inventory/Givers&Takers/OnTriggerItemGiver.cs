using UnityEngine;

namespace CatAttack.Inventory
{
	public class OnTriggerItemGiver : OnTriggerInventoryInteractor
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
		private void Awake ()
		{
			if (this.givenItems.Length == 0)
			{ Debug.LogWarning(string.Format("{0}.OnTriggerItemGiver no items given defined!!", this.gameObject.name)); }
		}
	//ENDOF MonoBehaviour

	//protected methods
		protected override void InteractWithInventory (IInventory inventory)
		{
			foreach (EItemID item in this.givenItems)
			{ inventory.Add(item); }
			this.disableOnPickup?.SetActive(false);
		}
	//ENDOF private methods
	}
}