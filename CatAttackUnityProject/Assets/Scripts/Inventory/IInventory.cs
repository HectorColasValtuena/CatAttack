namespace CatAttack.Inventory
{
	//interface representing an inventory manager
	//can query, add, and remove inventory items
	public interface IInventory
	{
		//returns true if item exists in inventory
		bool Contains (EItemID itemID);

		//adds item to inventory
		void Add (EItemID itemID);

		//removes item from inventory
		//returns true if item was removed, false if item didn't exist in inventory
		bool Remove (EItemID itemID);
	}
}