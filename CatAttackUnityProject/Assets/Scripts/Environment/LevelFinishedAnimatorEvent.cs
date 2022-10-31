namespace Components
{
	public class LevelFinishedAnimatorEvent : UnityEngine.MonoBehaviour
	{
		public void TriggerLevelFinished ()
		{ CatAttack.LevelManager.instance.AdvanceLevel(); }
	}
}
