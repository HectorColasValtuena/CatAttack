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
			this.defaultPose = new AccessoryPoseDefinition(
				masterSprite: null,
				localPosition: this.transform.localPosition,
				localRotation: this.transform.localRotation,
				accessorySprite: this.Sprite
			);
		}

		private void InitPoseDictionary ()
		{
			this.poseDictionary = new Dictionary<Sprite, AccessoryPoseDefinition>();
			foreach (AccessoryPoseDefinition pose in this.poses)
			{
				this.poseDictionary.Add(key: pose.MasterSprite, value: pose);
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
			this.transform.localPosition = pose.LocalPosition;
			this.transform.localRotation = pose.LocalRotation;
			this.Sprite = pose.AccessorySprite;
		}
	//ENDOF private methods

	//private sub-classes
		//Class defining the pose (sprite & transformation) of an accessory depending on the host sprite
		[System.Serializable]
		private class AccessoryPoseDefinition
		{
		//serialized fields
			[SerializeField]
			private Sprite _masterSprite;
			public Sprite MasterSprite
			{
				get { return this._masterSprite; }
				private set { this._masterSprite = value; }
			}

			[SerializeField]
			private Vector3 _localPosition;
			public Vector3 LocalPosition
			{
				get { return this._localPosition; }
				private set { this._localPosition = value; }
			}

			[SerializeField]
			private Vector3 _eulerAngles;
			private Quaternion? _localRotation = null;
			public Quaternion LocalRotation
			{
				get
				{
					if (this._localRotation == null) { this._localRotation = Quaternion.Euler(this._eulerAngles); }
					return (Quaternion) this._localRotation;
				}
				private set { this._localRotation = value; }
			}

			[SerializeField]
			private Sprite _accessorySprite;
			public Sprite AccessorySprite
			{
				get { return this._accessorySprite; }
				private set { this._accessorySprite = value; }
			}
		//ENDOF serialized

		//constructor
			public AccessoryPoseDefinition (
					Sprite masterSprite,
					Vector3 localPosition,
					Quaternion localRotation,
					Sprite accessorySprite
			) {
				this.MasterSprite = masterSprite;
				this.LocalPosition = localPosition;
				this.LocalRotation = localRotation;
				this.AccessorySprite = accessorySprite;
			}
		//ENDOF constructor
		}
	//ENDOF sub-classes
	}
}