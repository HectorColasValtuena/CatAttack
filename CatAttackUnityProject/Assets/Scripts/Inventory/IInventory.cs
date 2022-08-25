using System.Collections.Generic;

namespace CatAttack.Inventory
{
	//interface representing an inventory manager
	//can query, add, and remove inventory items
	public interface IInventory
	{
		//returns true if every item(s) exist in inventory. Returns false if any of the item(s) is missing.
		bool Contains (EItemID itemID);
		bool Contains (IList<EItemID> itemIDs);

		//adds item(s) to inventory
		void Add (EItemID itemID);
		void Add (IList<EItemID> itemIDs);

		//removes item(s) from inventory
		void Remove (EItemID itemID);
		void Remove (IList<EItemID> itemIDs);
	}
}