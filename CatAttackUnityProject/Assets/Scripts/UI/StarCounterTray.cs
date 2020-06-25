using UnityEngine;
using CatAttack;

namespace CatAttack.UI
{
	public class StarCounterTray : MonoBehaviour
	{
		public StarCounterSingle[] stars;
		public int activeStars;	//ammount of currently used stars
		public int fullStars; //ammount of stars that are full

		void OnEnable ()
		{
			Debug.Log("StarCounterTray.OnEnable()");
			//force-initialize ammount of active stars
			SetActiveStars(StarpowerReservoir.instance.maxStarpower);
			SetFullStars(StarpowerReservoir.instance.currentStarpower);
		}
		
		void Update ()
		{
			CheckActiveStars();
			CheckFullStars();
		}

		//check if ammount of stars full is as necessary. Update accordingly
		void CheckFullStars ()
		{
			if (fullStars != StarpowerReservoir.instance.currentStarpower)
			{
				SetFullStars(StarpowerReservoir.instance.currentStarpower);
			}
		}
		//set the ammount of full stars
		void SetFullStars (int target)
		{
			for (int i = 0, j = stars.Length; i < j; i++)
			{
				stars[i].full = target > i;
			}
			fullStars = target;
		}

		//check if ammount of stars activated is as necessary. Update accordingly
		void CheckActiveStars ()
		{
			if (activeStars != StarpowerReservoir.instance.maxStarpower)
			{
				SetActiveStars(StarpowerReservoir.instance.maxStarpower);
			}
		}
		//set the ammount of activated stars
		void SetActiveStars (int target)
		{
			for (int i = 0, j = stars.Length; i < j; i++)
			{
				stars[i].active = target > i;
			}
			activeStars = target;
		}
	}
}