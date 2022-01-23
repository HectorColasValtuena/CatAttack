namespace CatAttack.Accessories
{
	public interface IAccessory
	{
		//update this accessory's pose to fit parent sprite and horizontal flip
		void UpdatePose (UnityEngine.Sprite sprite, bool flip);
	}
}