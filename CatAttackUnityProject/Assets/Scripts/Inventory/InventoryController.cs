using System.Collections.Generic;
using UnityEngine;

namespace CatAttack.Inventory
{
	public class InventoryController :
		MonoBehaviour,
		IInventory
	{
	//IInventory implementation
		//returns true if item exists in inventory
		bool IInventory.Contains (EItemID itemID)
		{ return this.Contains(itemID); }
		private bool Contains (EItemID itemID)
		{ return this.Items.Contains(itemID); }

		//adds item to inventory
		void IInventory.Add (EItemID itemID)
		{ this.Add(itemID); }
		private void Add (EItemID itemID)
		{ 
			if (!this.Items.Contains(itemID))
			{ this.Items.Add(itemID); }
		}

		//removes item from inventory
		//returns true if item was removed, false if item didn't exist in inventory
		bool IInventory.Remove (EItemID itemID)
		{ return this.Remove(itemID); }
		private bool Remove (EItemID itemID)
		{ return this.Items.Remove(itemID); }
	//ENDOF IInventory implementation
	
	//MonoBehaviour lifecycle
		/*
		public void Awake ()
		{
		}
		//*/
	//ENDOF MonoBehaviour

	//private properties
		private ICollection<EItemID> Items
		{
			get
			{
				if (this._itemCollection == null) { this._itemCollection = new List<EItemID>(); }
				return this._itemCollection;
			}
		}
		private ICollection<EItemID> _itemCollection;
	//ENDOF private fields
	}
}