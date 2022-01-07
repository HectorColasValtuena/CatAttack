using UnityEngine;

public class AnimatorSetIsActivateOnProximity : MonoBehaviour
{
	[SerializeField]
	private Animator[] targets;

	public void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log("OnTriggerEnter2D");
		foreach (Animator animator in targets)
		{
			animator.SetBool("IsActive", true);
		}
	}
}
