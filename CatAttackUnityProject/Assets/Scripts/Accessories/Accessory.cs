using System.Collections.Generic;
using UnityEngine;

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
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private Vector3 rightwardScale = new Vector3(1, 1, 1);
		[SerializeField]
		private Vector3 leftwardScale = new Vector3(-1, 1, 1);
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
				{ this.gameObject.SetActive(value); }
			}
		}

		//inventory item represented by this accessory
		EItemID IAccessory.ItemID { get { return this.itemID; }}

		//update this accessory's pose to fit parent sprite and horizontal flip
		void IAccessory.UpdatePose (UnityEngine.Sprite masterSprite, bool flip)
		{ this.UpdatePose(masterSprite, flip); }
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

		private void UpdatePose (UnityEngine.Sprite masterSprite, bool flip)
		{
			if (this.poseDictionary.ContainsKey(masterSprite))
			{ this.ApplyPose(this.poseDictionary[masterSprite], flip); }
			else
			{ this.ApplyPose(this.defaultPose, flip); }

		}

		private void ApplyPose (AccessoryPoseDefinition pose, bool flip)
		{
			this.transform.localPosition = pose.localPosition;
			this.Sprite = pose.accessorySprite;
			this.transform.localScale = flip ? this.leftwardScale : this.rightwardScale;
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