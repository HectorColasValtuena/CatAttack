using UnityEngine;

namespace CatAttack
{
	public class DynamiteQuestPhase1Advancer : MonoBehaviour
	{
		public DynamiteQuestController quest;

		public void Awake ()
		{
			if (quest == null) { quest = transform.root.GetComponent<DynamiteQuestController>(); }
		}

		public void OnTriggerExit2D (Collider2D other)
		{
			if (other.tag != "Player") { Debug.LogWarning ("[DynamiteQuestPhase1Advancer] non-player collision exit detected! "); return; }
			quest.AdvancePhase(2);
		}
	}
}