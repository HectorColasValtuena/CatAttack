using System.Collections.Generic;
using UnityEngine;
using UnityEvent = UnityEngine.Events.UnityEvent;

using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Accessories
{
	//class managing a single accessory's visual component. managed by its parent AccessoriesController
	public class Accessory :
		MonoBehaviour,
		IAccessory
	{
	//serialized fields
		[SerializeField]
		private EItemID itemID;

		[SerializeField]
		private AccessoryPoseDefinition[] poses;

		[SerializeField]
		private bool removeItemOnDeath;

		[SerializeField]
		private UnityEvent onCarrierDeath;

		[SerializeField]
		private SpriteRenderer spriteRenderer;
	//ENDOF serialized fields

	//IAccessory
		//gets and sets wether this accessory is active
		bool IAccessory.Enabled { get { return this.Enabled; } set { this.Enabled = value; }}
		private bool Enabled
		{
			get { return this.gameObject.activeSelf; }
			set
			{
				if (this.gameObject.activeSelf != value)
				{
					this.gameObject.SetActive(value);
					if (value)
					{ this.OnEnableRefresh(); }
				}
			}
		}

		//inventory item represented by this accessory
		EItemID IAccessory.ItemID { get { return this.itemID; }}

		//if true the linked item should be removed from the inventory on character death
		bool IAccessory.RemoveItemOnDeath { get { return this.removeItemOnDeath; }}

		//update this accessory's pose to fit parent sprite and horizontal flip
		void IAccessory.UpdatePose (UnityEngine.Sprite masterSprite)
		{ this.UpdatePose(masterSprite); }

		//to be called on player death
		void IAccessory.OnCarrierDeath ()
		{ this.onCarrierDeath?.Invoke(); }
	//ENDOF IAccessory

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.spriteRenderer == null) { Debug.LogError(this.name + ".SpriteRenderer not set!"); }
			this.StoreDefaults();
			this.InitPoseDictionary();
		}
	//ENDOF MonoBehaviour

	//private properties
		private Sprite Sprite
		{
			get { return this.spriteRenderer.sprite; }
			set { this.spriteRenderer.sprite = value; }
		}
	//ENDOF private properties

	//private fields
		private AccessoryPoseDefinition defaultPose;
		private IDictionary<Sprite, AccessoryPoseDefinition> poseDictionary;
		private Sprite masterSprite;
	//ENDOF private fields

	//private methods
		private void StoreDefaults ()
		{
			this.defaultPose = new AccessoryPoseDefinition();
			this.defaultPose.localPosition = this.transform.localPosition;
			this.defaultPose.accessorySprite = this.Sprite;
		}

		private void InitPoseDictionary ()
		{
			this.poseDictionary = new Dictionary<Sprite, AccessoryPoseDefinition>();
			foreach (AccessoryPoseDefinition pose in this.poses)
			{
				this.poseDictionary.Add(key: pose.masterSprite, value: pose);
			}
		}

		private void OnEnableRefresh()
		{ this.UpdatePose(this.masterSprite); }

		private void UpdatePose (UnityEngine.Sprite masterSprite)
		{
			this.masterSprite = masterSprite;
			if (!this.Enabled) { return; } //optimize by not updating disabled accessories

			if (this.poseDictionary.ContainsKey(masterSprite))
			{ this.ApplyPose(this.poseDictionary[masterSprite]); }
			else
			{ this.ApplyPose(this.defaultPose); }

		}

		private void ApplyPose (AccessoryPoseDefinition pose)
		{
			this.transform.localPosition = pose.localPosition;
			this.Sprite = pose.accessorySprite;
		}
	//ENDOF private methods

	//private sub-classes
		//Class defining the pose (sprite & transformation) of an accessory depending on the host sprite
		[System.Serializable]
		private class AccessoryPoseDefinition
		{
		//serialized fields
			[SerializeField]
			public Sprite masterSprite;
			[SerializeField]
			public Vector3 localPosition;
			[SerializeField]
			public Sprite accessorySprite;
		//ENDOF serialized
		}
	//ENDOF sub-classes
	}
}