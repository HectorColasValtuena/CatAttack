using UnityEngine;

public class DisableOtherOnMyEnable : MonoBehaviour
{
	public GameObject targetGameObject;

	public void OnEnable () 
	{
		targetGameObject.SetActive(false);
	}

	public void OnDisable ()
	{
		targetGameObject.SetActive(true);
	}
}
