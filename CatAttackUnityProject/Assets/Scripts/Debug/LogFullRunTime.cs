using UnityEngine;
using CatAttack;

public class LogFullRunTime : MonoBehaviour
{
	public void Awake ()
	{
		if (RunTimeAccumulator.FullRunAvailable)
		{ Debug.Log("Full run time: " + RunTimeAccumulator.FullRunCombinedTime); }
		else
		{ Debug.Log("Full run time NOT AVAILABLE"); }
	}
}
