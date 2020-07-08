using UnityEngine;

namespace CatAttack
{
	public class DynamiteQuestController : MonoBehaviour
	{
		public GameObject[] dialogList;	//list of dialog containers for respective phases
		public Animator animator;

		//No, this is dealt with by the animation
		//public GameObject rubbleBlockage;	//gameobject of the rubble pile blocking the path - disable upon completion
		//public GameObject explosionFX;		//effects to play upon blowing up the rubble

		private int phase = 0;

		public void Awake ()
		{
			phase = 0;
			if (animator == null) { animator = GetComponent<Animator>(); }
		}

		//Advances the phase index and enables target gameobject within the list. if parameter is negative or omitted will automatically advance 1 step
		public void AdvancePhase (int targetPhase = -1)
		{
			if (targetPhase < 0) { targetPhase = phase+1; }
			if (targetPhase <= phase) { return; }
			phase = targetPhase;
			ChangeDialog(targetPhase);

			//phase specific checks - optimize this?
			if (phase == 2)
			{
				//Blow-up the wall upon reaching phase 2
				//play the animation blowing up the wall
				animator.SetBool("DynamiteBrought", true);
			}
			//else if (phase == 3)
		}

		private void ChangeDialog (int targetDialog)
		{
			for (int i = 0, iLimit = dialogList.Length; i < iLimit; i++)
			{
				dialogList[i].SetActive(i == targetDialog);
			}
		}
	}
}