using UnityEngine;

using IInventory = CatAttack.Inventory.IInventory;
using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Quests
{
	public class FetchQuestController : QuestControllerBase
	{
	//serialized fields
		[Tooltip("list of items required by the quest - will be removed from the inventory")]
		[SerializeField]
		private EItemID[] requiredItems;

		[Tooltip("Disables these objects when quest is completed, like popup dialog or related triggers")]
		[SerializeField]
		private GameObject[] disableOnCompleted;

		[Tooltip("Enables these objects when quest is completed")]
		[SerializeField]
		private GameObject[] enableOnCompleted;

		[Tooltip("Instantiates copies of these objects when quest is completed, like FX prefabs")]
		[SerializeField]
		private GameObject[] instantiateOnCompleted;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.requiredItems.Length == 0)
			{ Debug.LogWarning(string.Format("{0}.FetchQuestController no items required defined!!", this.gameObject.name)); }
		}

		//when a trigger linked to this object enters contact with anything with an inventory, check its inventory for the required item
		//use physics layers to avoid unwanted triggers
		private void OnTriggerEnter2D (Collider2D other)
		{
			IInventory inventory = other.GetComponent<IInventory>();
			if (inventory == null) { return; }
			if (this.GetItemsRequired(inventory) == true)
			{
				this.SetCompleted(inventory);
			}
		}
	//ENDOF MonoBehaviour

	//overrides
		protected override void OnCompleted ()
		{
			foreach (GameObject disableItem in this.disableOnCompleted)
			{ disableItem.SetActive(false); }

			foreach (GameObject enableItem in this.enableOnCompleted)
			{ enableItem.SetActive(true); }

			foreach (GameObject instantiateObjec in this.instantiateOnCompleted)
			{ 	
				UnityEngine.Object.Instantiate(
					original: instantiateObjec,
					position: this.transform.position,
					rotation: Quaternion.identity,
					parent: null
				);
			}
		}
	//ENDOF overrides

	//private methods
		//check if items required are in given inventory, and remove them from inventory
		//returns false if not all items required are available
		private bool GetItemsRequired (IInventory inventory)
		{
			//first check that all required items are available
			foreach (EItemID item in this.requiredItems)
			{
				if (!inventory.Contains(item))
				{
					//if any item is missing, return false
					return false;
				}
			}

			//then remove those items from the inventory
			foreach (EItemID item in this.requiredItems)
			{ inventory.Remove(item); }

			//return true after required items correctly removed
			return true;
		}
	//ENDOF private
	}
}