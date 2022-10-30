using UnityEngine;

namespace CatAttack.FX
{
	//When anything enters this trigger set desired bool variable on target animator to true
	public class SetAnimatorBoolOnTrigger : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private string boolName = "VariableName";

		[SerializeField]
		private bool valueSetOnTrigger = true;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void OnTriggerEnter2D (Collider2D other)
		{
			if (this.animator == null) { Debug.LogError("" + this.name + ".AnimatorSetBoolOnTrigger animator not set!"); }
			this.animator.SetBool(this.boolName, this.valueSetOnTrigger);
		}
	//ENDOF MonoBehaviour
	}
}
