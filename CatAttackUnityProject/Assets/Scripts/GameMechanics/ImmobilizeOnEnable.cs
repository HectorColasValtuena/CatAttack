using UnityEngine;

namespace CatAttack
{
	public class ImmobilizeOnEnable : MonoBehaviour
	{
		public bool onlyOnce = true;	//will destroy self after triggering if true

		public void OnEnable ()
		{
			LevelManager.playerGameObject.Immobilize();
			if (onlyOnce) { Destroy(this); }
		}
	}
}