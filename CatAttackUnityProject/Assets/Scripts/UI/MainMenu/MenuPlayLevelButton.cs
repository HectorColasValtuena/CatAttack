﻿using UnityEngine;

namespace CatAttack
{
	public class MenuPlayLevelButton : MonoBehaviour
	{
		public int levelNumber;
		public bool forced = false;

		public void Start ()
		{
			if (!ProgressionManager.LevelIsUnlocked(levelNumber) && !forced)
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
