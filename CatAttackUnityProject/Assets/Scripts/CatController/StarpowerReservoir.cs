using UnityEngine;

namespace CatAttack
{
	public class StarpowerReservoir : MonoBehaviour
	{
		public static StarpowerReservoir instance;

		private int currentStarpower;
		public int maxStarpower = 4;

		public void Awake ()
		{ 
			Debug.Log("StarpowerReservoir.Awake()");
			StarpowerReservoir.instance = this;
			currentStarpower = maxStarpower;
		}

		//check if there's enough starpower. default 1. returns true/false
		public bool HasStarpower (int targetAmmount = 1)
		{
			return currentStarpower >= targetAmmount;
		}

		//attempts to drain target ammount. default 1. Returns true if succeeded
		public bool DrainStarpower (int targetAmmount = 1)
		{
			//return true;

			if (HasStarpower(targetAmmount))
			{
				currentStarpower -= targetAmmount;
				return true;
			}
			return false;
		}

		//regains given ammount of starpower. Maximum if parameter ommited.
		public void RegenerateStarpower (int targetAmmount = -1)
		{
			//restore up to maximum if ommited or negative
			if (targetAmmount < 0) 
			{
				currentStarpower = maxStarpower;
			}
			else
			{
			//else restore given ammount up to maximum
				currentStarpower += targetAmmount;
				if (currentStarpower > maxStarpower) 
				{
					currentStarpower = maxStarpower;
				}
			}
		}

		public void GainMaximumStarpower (int targetAmmount = 1)
		{
			maxStarpower += targetAmmount;
			currentStarpower += targetAmmount;
		}

		public void SetMaximumStarpower (int targetAmmount = 1)
		{
			maxStarpower = targetAmmount;
			currentStarpower = targetAmmount;
		}
	}
}