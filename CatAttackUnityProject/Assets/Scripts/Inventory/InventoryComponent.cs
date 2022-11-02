using System.Collections.Generic;

using UnityEngine;

namespace CatAttack.Inventory
{
	public class InventoryComponent : MonoBehaviour, IInventory
	{
	//IInventory implementation
		//returns true if every item(s) exist in inventory. Returns false if any of the item(s) is missing.
		bool IInventory.Contains (EItemID itemID) { return RunManager.RunInventory.Contains(itemID); }
		bool IInventory.Contains (IList<EItemID> itemIDs) { return RunManager.RunInventory.Contains(itemIDs); }

		//adds item to inventory
		void IInventory.Add (EItemID itemID) { RunManager.RunInventory.Add(itemID); }
		void IInventory.Add (IList<EItemID> itemIDs) { RunManager.RunInventory.Add(itemIDs); }

		//removes item from inventory
		void IInventory.Remove (EItemID itemID) { RunManager.RunInventory.Remove(itemID); }
		void IInventory.Remove (IList<EItemID> itemIDs) { RunManager.RunInventory.Remove(itemIDs); }
	//ENDOF IInventory implementation
	}
}
