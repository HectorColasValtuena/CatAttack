using UnityEngine;

namespace CatAttack.Inventory
{
	public class OnTriggerItemGiver : OnTriggerInventoryInteractor
	{
	//serialized fields
		[Tooltip("List of items given to the player")]
		[SerializeField]
		private EItemID[] givenItems;

		[Tooltip("These UnityEngine.Object will be disabled once item is picked up")]
		[SerializeField]
		private UnityEngine.Object[] disableOnPickup;

		[Tooltip("Will spawn a copy of these objects when items are added to an inventory")]
		[SerializeField]
		private GameObject[] spawnObjects;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.givenItems == null || this.givenItems.Length == 0)
			{ Debug.LogWarning(string.Format("{0}.OnTriggerItemGiver no items given defined!!", this.gameObject.name)); }
		}
	//ENDOF MonoBehaviour

	//protected methods
		protected override void InteractWithInventory (IInventory inventory)
		{
			if (!inventory.Contains(this.givenItems))
			{
				inventory.Add(givenItems);
				this.DisableObjects();
				this.SpawnObjects();
			}
		}
	//ENDOF protected methods

	//private methods
		private void DisableObjects ()
		{
			foreach (UnityEngine.Object disableObject in this.disableOnPickup)
			{
				disableObject.SetActive(false);
			}
		}

		private void SpawnObjects ()
		{
			foreach (GameObject spawnObject in this.spawnObjects)
			{
				UnityEngine.Object.Instantiate(spawnObject, this.transform, this.transform.position);
			}
		}
	//ENDOF private methods		
	}
}