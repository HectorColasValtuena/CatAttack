using UnityEngine;

namespace CatAttack
{
	public class StopwatchPickup : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Animator animator;

	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.animator == null) { this.animator = this.gameObject.GetComponent<Animator>(); }
		}

		private void Start ()
		{
			if (ProgressionManager.StopwatchUnlocked)
			{
				this.Disable();
			}
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			this.PickedUp();
			Debug.Log("Trigger enter");
		}
	//ENDOF MonoBehaviour

	//private methods
		private void PickedUp ()
		{
			this.animator.SetTrigger("PickedUp");
			ProgressionManager.UnlockStopwatch();
		}

		private void Disable ()
		{
			this.animator.SetBool("Disabled", true);
		}
	//ENDOF private methods
	}
}