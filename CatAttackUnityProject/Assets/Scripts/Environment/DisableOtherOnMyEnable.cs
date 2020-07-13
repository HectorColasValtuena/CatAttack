using UnityEngine;

public class DisableOtherOnMyEnable : MonoBehaviour
{
	public GameObject targetGameObject;

	public bool reEnableOnMyDisable = true;

	public void OnEnable () 
	{
		targetGameObject.SetActive(false);
	}

	public void OnDisable ()
	{
		if (reEnableOnMyDisable) { targetGameObject.SetActive(true); }
	}
}
