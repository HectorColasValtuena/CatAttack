using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatAttack
{
	public static class ProgressionManager
	{
	//constants
		private const string levelUnlockedKey = "levelUnlocked";
		private const int defaultUnlockedLevel = 3;
	//ENDOF constants

	//public fields
		public static int currentLevel = 1;
	//ENDOF public field

	//public methods
		//get if scene is unlocked
		public static bool LevelIsUnlocked (int levelNumber)
		{
			//JSUT IN CSAE guaranteed true on 1 or less
			if (levelNumber <= defaultUnlockedLevel) return true;
			
			//If playerprefs is uninitialized, set up default value
			if (!PlayerPrefs.HasKey(levelUnlockedKey)) { PlayerPrefs.SetInt(levelUnlockedKey, defaultUnlockedLevel); } // return levelNumber <= defaultUnlockedLevel; }
			return levelNumber <= PlayerPrefs.GetInt(levelUnlockedKey);
		}

		//unlock target scene
		public static void LevelUnlock (int levelNumber)
		{
			//only write playerprefs if not unlocked already to avoid closing later levels
			if (!ProgressionManager.LevelIsUnlocked(levelNumber)) { PlayerPrefs.SetInt(levelUnlockedKey, levelNumber); }
		}

		//unlock the level after current level
		public static void UnlockNext ()
		{
			LevelUnlock(currentLevel + 1);
		}

		//open target scene
		public static void LevelChange (int levelNumber, bool autoUnlock = true)
		{
			if (autoUnlock) { ProgressionManager.LevelUnlock(levelNumber); }
			currentLevel = levelNumber;
			SceneManager.LoadScene(levelNumber);		
		}

		//open next scene
		public static void AdvanceLevel (bool autoUnlock = true)
		{
			ProgressionManager.LevelChange(currentLevel+1, autoUnlock);
		}

		//resets game save
		public static void ResetSave ()
		{
			currentLevel = 1;
			PlayerPrefs.DeleteAll();
			LevelChange(1);
		}
	//ENDOF public methods

	//private methods
	//ENDOF private methods
	}
}