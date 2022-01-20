namespace CatAttack
{
	public class TNTQuestController : FetchQuestControllerBase
	{
	//overrides
		protected override void GiveReward (InventoryController inventory) {}

		protected override bool GetQuestItemFromInventory (InventoryController inventory)
		{
			if (inventory.Dynamite)
			{
				inventory.Dynamite = false;
				return true;
			}
			return false;
		}
	//ENDOF overrides
	}
}