using UnityEngine;
using CatAttack;

public class LogFullRunTime : MonoBehaviour
{
	public void Awake ()
	{
		if (RunManager.FullRunAvailable)
		{ Debug.Log("Full run time: " + RunManager.FullRunCombinedTime); }
		else
		{ Debug.Log("Full run time NOT AVAILABLE"); }
	}
}
