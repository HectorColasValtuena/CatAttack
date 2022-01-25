using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Accessories
{
	public interface IAccessory
	{
		//gets and sets wether this accessory is active
		bool Enabled { get; set; }

		//inventory item represented by this accessory
		EItemID ItemID { get; }

		//update this accessory's pose to fit parent sprite and horizontal flip
		void UpdatePose (UnityEngine.Sprite masterSprite);
	}
}