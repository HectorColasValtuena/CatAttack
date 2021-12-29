using UnityEngine;

namespace CatAttack
{
	public class VictoryZone : MonoBehaviour
	{
		private static void PlayerWon (GameObject target)
		{
			PlatformerCharacter2D playerController = target.GetComponent<PlatformerCharacter2D>();
			if (playerController != null && !playerController.m_IsAsleep) 
			{
				playerController.FallAsleep();
				LevelManager.instance.LevelFinished();
			}
		}

		public void OnTriggerEnter2D (Collider2D other) { VictoryZone.PlayerWon(other.gameObject); }
		public void OnCollisionEnter2D (Collision2D other) { VictoryZone.PlayerWon(other.gameObject); }

	}
}