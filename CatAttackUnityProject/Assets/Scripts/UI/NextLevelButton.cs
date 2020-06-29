using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAttack
{
	public class NextLevelButton : MonoBehaviour
	{
		public void NextLevelButtonPressed ()
		{
			LevelManager.instance.AdvanceLevel();
		}
	}
}