using UnityEngine;

namespace CatAttack
{
	public class DynamiteQuestPhase0Advancer : MonoBehaviour
	{
		public DynamiteQuestController quest;

		public void Awake ()
		{
			if (quest == null) { quest = transform.root.GetComponent<DynamiteQuestController>(); }
		}

		public void OnTriggerEnter2D (Collider2D other)
		{
			InventoryController inventory = other.GetComponent<InventoryController>();
			if (inventory == null) { return; }
			if (inventory.hasDynamite)
			{
				inventory.hasDynamite = false;
				quest.AdvancePhase(1);
			}
		}
	}
}