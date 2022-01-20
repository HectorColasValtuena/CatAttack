using UnityEngine;

namespace CatAttack
{
	public abstract class QuestControllerBase :
		MonoBehaviour,
		IQuest
	{
	//serialized fields
		[SerializeField]
		private Animator animator;	//animator used to play completion animation
	//ENDOF serialized fields

	//IQuest
		//true if quest has been fulfilled
		bool IQuest.Completed { get { return this.Completed; }}
		private bool Completed
		{ get; set; }
	//ENDOF IQuest

	//protected class methods
		protected void SetCompleted (InventoryController inventory)
		{
			if(!this.Completed)
			{
				this.Completed = true;
				this.OnCompleted(inventory);
			}
		}

		private void OnCompleted (InventoryController inventory)
		{
			this.Cleanup();
			this.GiveReward(inventory);
			this.PlayCompletedAnimation();
		}
	//ENDOF class methods

	//abstract methods
		protected abstract void Cleanup ();
		protected abstract void GiveReward (InventoryController inventory);
	//ENDOF abstract

	//private methods
		private void PlayCompletedAnimation ()
		{
			if (this.animator == null) { this.animator = this.gameObject.GetComponent<Animator>(); }
			animator?.SetBool("QuestCompleted", true);
		}
	//ENDOF private methods
	}
}