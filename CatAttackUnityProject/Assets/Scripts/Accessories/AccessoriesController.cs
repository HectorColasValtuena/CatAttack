using System.Collections.Generic;
using UnityEngine;

using IInventory = CatAttack.Inventory.IInventory;
using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Accessories
{
	//controller class managing a list of Accessory objects
	//this class updates the accessories giving them sprite information
	public class AccessoriesController : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Accessory[] accessories;

		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private CatAttack.Inventory.InventoryController _inventory;
		private IInventory Inventory { get { return this._inventory; }}
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.spriteRenderer == null) { Debug.LogError("!! " + this.gameObject.name + ".AccessoriesController: ERROR no spriteRenderer defined"); }
			if (this.Inventory == null) { Debug.LogError("!! " + this.gameObject.name + ".AccessoriesController: ERROR no inventory defined"); }
		}

		private void Update ()
		{
			this.UpdateEnabledAccessories();
			this.CheckPoseUpdate();
		}
	//ENDOF MonoBehaviour lifecycle

	//private properties
		//info on current pose
		private Sprite CurrentSprite 
		{ get { return this.spriteRenderer.sprite; }}
		private bool CurrentFlip
		{ get { return this.spriteRenderer.flipX; }}
	//ENDOF private properties

	//private fields
		//info on last pose
		private Sprite lastSprite = null;
		private bool lastFlip = false;
	//ENDOF private fields

	//private methods
		//updates which accessories are active according to player's inventory
		private void UpdateEnabledAccessories ()
		{
			foreach (IAccessory accessory in this.accessories)
			{
				accessory.Enabled = this.Inventory.Contains(accessory.ItemID);
			}
		}

		//updates accessories by pose if pose has changed
		private void CheckPoseUpdate ()
		{
			if ((this.lastSprite != this.CurrentSprite) || (this.lastFlip != this.CurrentFlip))
			{ this.UpdateAccessoryPoses(); }

			this.lastSprite = this.CurrentSprite;
			this.lastFlip = this.CurrentFlip;
		}

		//update every accessory pose
		private void UpdateAccessoryPoses ()
		{
			foreach (IAccessory accessory in this.accessories)
			{
				if (accessory.Enabled)
				{ accessory.UpdatePose(masterSprite: this.CurrentSprite, flip: this.CurrentFlip); }
			}
		}
	//ENDOF private methods
	}
}