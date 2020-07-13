using UnityEngine;

public class EnableOtherOnMyEnable : MonoBehaviour
{
	public GameObject targetGameObject;

	public bool reDisableOnMyDisable = true;

	public void OnEnable () 
	{
		targetGameObject.SetActive(true);
	}

	public void OnDisable ()
	{
		if (reDisableOnMyDisable) { targetGameObject.SetActive(false); }
	}
}
