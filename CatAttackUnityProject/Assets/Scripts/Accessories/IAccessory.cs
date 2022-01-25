using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Accessories
{
	public interface IAccessory
	{
		//gets and sets wether this accessory is active
		bool Enabled { get; set; }

		//inventory item represented by this accessory
		EItemID ItemID { get; }

		//if true the linked item should be removed from the inventory on character death
		bool RemoveItemOnDeath { get; } //I don't like this a lot but it works I guess

		//update this accessory's pose to fit parent sprite and horizontal flip
		void UpdatePose (UnityEngine.Sprite masterSprite);

		//to be called on player death
		void OnCarrierDeath ();
	}
}