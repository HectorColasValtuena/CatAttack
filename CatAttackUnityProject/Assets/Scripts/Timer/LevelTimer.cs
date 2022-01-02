using UnityEngine;

namespace CatAttack
{
	//class handling the timing within a single level
	public class LevelTimer : MonoBehaviour
	{
	//public properties
		public float levelTime
		{
			get 
			{
				return this.fixedUpdateCount * Time.fixedDeltaTime;
			}
		}
	//ENDOF public properties

	//public methods
		public void StartTimer (bool reset = true)
		{
			if (reset && !this.timerActive)
			{ this.ResetTimer(); }
			this.timerActive = true;
		}

		public void ResetTimer (bool stop = true)
		{
			if (stop)
			{ this.StopTimer(); }
			this.fixedUpdateCount = 0;
		}

		public void StopTimer ()
		{
			this.timerActive = false;
		}
	//ENDOF public methods

	//MonoBehaviour lifecycle
		public void FixedUpdate ()
		{
			if (this.timerActive)
			{
				this.fixedUpdateCount++;
			}
		}
	//ENDOF MonoBehaviour lifecycle

	//private fields
		private ulong fixedUpdateCount = 0; //proper timer, counts # of FixedUpdates
		private bool timerActive = false;
	//ENDOF private fields

	//private methods
	//ENDOF private methods
	}
}