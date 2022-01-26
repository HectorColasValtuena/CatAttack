using UnityEngine;

namespace CatAttack.Inventory
{
	public class OnTriggerItemTaker : OnTriggerInventoryInteractor
	{
	//serialized fields
		//list of items given to the player
		[SerializeField]
		private EItemID[] takenItems;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.takenItems.Length == 0)
			{ Debug.LogWarning(string.Format("{0}.OnTriggerItemTaker no items taken defined!!", this.gameObject.name)); }
		}
	//ENDOF MonoBehaviour

	//protected methods
		protected override void InteractWithInventory (IInventory inventory)
		{
			foreach (EItemID item in this.takenItems)
			{ inventory.Remove(item); }
		}
	//ENDOF private methods
	}
}