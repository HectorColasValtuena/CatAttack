﻿using System.Collections.Generic;
using UnityEngine;

namespace CatAttack.Inventory
{
	public class InventoryController :
		MonoBehaviour,
		IInventory
	{
	//IInventory implementation
		//returns true if every item(s) exist in inventory. Returns false if any of the item(s) is missing.
		bool IInventory.Contains (EItemID itemID) { return this.Contains(itemID); }
		bool IInventory.Contains (IList<EItemID> itemIDs) { return this.Contains(itemIDs); }

		private bool Contains (EItemID itemID)
		{ return this.Items.Contains(itemID); }
		private bool Contains (IList<EItemID> itemIDs)
		{
			foreach (EItemID itemID in itemIDs)
			{ if (!this.Contains(itemID)) { return false; }}
			return true;
		}

		//adds item to inventory
		void IInventory.Add (EItemID itemID) { this.Add(itemID); }
		void IInventory.Add (IList<EItemID> itemIDs) { this.Add(itemIDs); }

		private void Add (EItemID itemID)
		{ 
			if (!this.Items.Contains(itemID))
			{ this.Items.Add(itemID); }
		}
		private void Add (IList<EItemID> itemIDs)
		{
			foreach (EItemID itemID in itemIDs)
			{ this.Add(itemID);	}
		}

		//removes item from inventory
		//returns true if item was removed, false if item didn't exist in inventory
		void IInventory.Remove (EItemID itemID) { this.Remove(itemID); }
		void IInventory.Remove (IList<EItemID> itemIDs) { this.Remove(itemIDs); }

		private void Remove (EItemID itemID)
		{ this.Items.Remove(itemID); }
		private void Remove (IList<EItemID> itemIDs)
		{
			foreach (EItemID itemID in itemIDs)
			{ this.Remove(itemID); }
		}
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