using System.Collections.Generic;
using UnityEngine;


using IPlayer = CatAttack.Player.IPlayer;
using IInventory = CatAttack.Inventory.IInventory;
using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Accessories
{
	//controller class managing a list of Accessory objects
	//this class updates the accessories giving them sprite information
	public class AccessoriesController : MonoBehaviour
	{
	//constants
	//ENDOF constants

	//serialized fields
		[SerializeField]
		private Accessory[] accessories;

		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private PlatformerCharacter2D _player;
		private IPlayer Player { get { return this._player; }}

		private IInventory Inventory { get { return RunManager.RunInventory; }}
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.spriteRenderer == null) { Debug.LogError("!! " + this.gameObject.name + ".AccessoriesController: ERROR no spriteRenderer defined"); }
			if (this.Inventory == null) { Debug.LogError("!! " + this.gameObject.name + ".AccessoriesController: ERROR no inventory defined"); }
			if (this.Player == null) { Debug.LogError("!! " + this.gameObject.name + ".AccessoriesController: ERROR no player defined"); }
		}

		private void Update ()
		{
			this.CheckPlayerDeath();
			this.UpdateEnabledAccessories();
			this.CheckFlipUpdate();
			this.CheckPoseUpdate();
		}
	//ENDOF MonoBehaviour lifecycle

	//private properties
		//info on current pose
		private Sprite CurrentSprite 
		{ get { return this.spriteRenderer.sprite; }}
		private bool CurrentFlip
		{ get { return this.spriteRenderer.flipX; }}

		private bool PlayerIsDead
		{ get { return this.Player.IsDead; }}

		//info on default scales for each flip direction
		private Vector3 rightwardScale = new Vector3(1, 1, 1);
		private Vector3 leftwardScale = new Vector3(-1, 1, 1);
	//ENDOF private properties

	//private fields
		//info on last pose
		private Sprite lastSprite = null;
		private bool lastFlip = false;
		private bool lastIsPlayerDead = false;
	//ENDOF private fields

	//private methods
		private void CheckPlayerDeath ()
		{
			if (this.PlayerIsDead && !this.lastIsPlayerDead)
			{ this.OnPlayerDeath(); } 
		}

		private void OnPlayerDeath ()
		{
			foreach (IAccessory accessory in this.accessories)
			{
				accessory.OnCarrierDeath();
				if (accessory.RemoveItemOnDeath)
				{ this.Inventory.Remove(accessory.ItemID); }
			}
		}

		//updates which accessories are active according to player's inventory
		//every item will be made disabled during player death
		private void UpdateEnabledAccessories ()
		{
			foreach (IAccessory accessory in this.accessories)
			{
				accessory.Enabled =
						this.Inventory.Contains(accessory.ItemID)
					&&	!this.PlayerIsDead;
			}
		}

		private void CheckFlipUpdate ()
		{
			if (this.lastFlip != this.CurrentFlip)
			{
				this.transform.localScale = this.CurrentFlip
					? this.leftwardScale
					: this.rightwardScale;
			}

			this.lastFlip = this.CurrentFlip;
		}

		//updates accessories by pose if pose has changed
		private void CheckPoseUpdate ()
		{
			if (this.lastSprite != this.CurrentSprite)
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
				{ accessory.UpdatePose(masterSprite: this.CurrentSprite); }
			}
		}
	//ENDOF private methods
	}
}