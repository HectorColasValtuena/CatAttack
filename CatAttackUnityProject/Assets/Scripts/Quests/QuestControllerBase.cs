using UnityEngine;

using IInventory = CatAttack.Inventory.IInventory;
using EItemID = CatAttack.Inventory.EItemID;

namespace CatAttack.Quests
{
	public abstract class QuestControllerBase :
		MonoBehaviour,
		IQuest
	{
	//serialized fields
		//list of items given to the player on completion
		[SerializeField]
		private EItemID[] rewards;

		//animator used to play completion animation
		[SerializeField]
		private Animator animator;
	//ENDOF serialized fields

	//IQuest
		//true if quest has been fulfilled
		bool IQuest.Completed { get { return this.Completed; }}
		private bool Completed { get; set; }
	//ENDOF IQuest

	//abstract methods
	//ENDOF abstract

	//virtual methods
		protected virtual void OnCompleted () {}
	//ENDOF virtual

	//protected class methods
		protected void SetCompleted (IInventory inventory)
		{
			if(!this.Completed)
			{
				this.Completed = true;
				this.GiveReward(inventory);
				this.PlayCompletedAnimation();

				this.OnCompleted();
			}
		}
	//ENDOF class methods

	//private methods
		private void GiveReward (IInventory inventory)
		{
			foreach (EItemID item in this.rewards)
			{
				inventory.Add(item);
			}
		}

		private void PlayCompletedAnimation ()
		{
			if (this.animator == null) { this.animator = this.gameObject.GetComponent<Animator>(); }
			animator?.SetBool("QuestCompleted", true);
		}
	//ENDOF private methods
	}
}