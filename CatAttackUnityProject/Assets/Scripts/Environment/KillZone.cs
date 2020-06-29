using UnityEngine;

namespace CatAttack
{
	public class KillZone : MonoBehaviour
	{
		private static void Kill (GameObject target)
		{
			PlatformerCharacter2D targetController = target.GetComponent<PlatformerCharacter2D>();
			if (targetController != null) { targetController.Death(); }
		}

		public void OnTriggerEnter2D (Collider2D other) { KillZone.Kill(other.gameObject); }
		public void OnCollisionEnter2D (Collision2D other) { KillZone.Kill(other.gameObject); }

	}
}