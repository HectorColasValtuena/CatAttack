using UnityEngine;

namespace CatAttack
{
	public class ResetExecutor : MonoBehaviour
	{
		public void ExecuteReset ()
		{
			CatAttack.LevelManager.instance.ResetPlayer();
		}
	}
}