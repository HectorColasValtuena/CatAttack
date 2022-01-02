using UnityEngine;

namespace CatAttack
{
	//Resets the run time accumulator to avoid cross run alteration
	public class OnAwakeRunTimerResetter : MonoBehaviour
	{
		public void Awake ()
		{ RunTimeAccumulator.ResetRun(); }
	}
}