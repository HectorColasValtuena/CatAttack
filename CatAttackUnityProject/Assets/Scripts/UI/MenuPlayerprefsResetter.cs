using UnityEngine;

namespace CatAttack
{
	public class MenuPlayerprefsResetter : MonoBehaviour
	{
		public void ResetPlayerprefsButton()
		{
			Debug.LogWarning("ResettingPlayerprefs");
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
			ProgressionManager.LevelChange(0);
		}
	}
}