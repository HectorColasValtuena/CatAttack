using UnityEngine;

namespace CatAttack
{
	public class DeImmobilizeOnEnable : MonoBehaviour
	{
		public void OnEnable ()
		{
			LevelManager.playerGameObject.DeImmobilize();
		}
	}
}