using UnityEngine;

namespace CatAttack
{
	public class MenuPlayLevelButton : MonoBehaviour
	{
		public int levelNumber;

		public void Start ()
		{
			if (!ProgressionManager.LevelIsUnlocked(levelNumber))
			{
				gameObject.SetActive(false);
			}
		}

		public void PlayLevelButtonPressed ()
		{
			ProgressionManager.LevelChange(levelNumber);
		}
	}
}
