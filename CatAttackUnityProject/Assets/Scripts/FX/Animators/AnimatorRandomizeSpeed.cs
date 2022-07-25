using SerializeField = UnityEngine.SerializeField;

using Animator = UnityEngine.Animator;
using Random = UnityEngine.Random;

using RandomFloatRange = PHATASS.Utils.Types.Ranges.RandomFloatRange;
using ILimitedRangeFloat = PHATASS.Utils.Types.Ranges.ILimitedRange<float>;

namespace CatAttack.FX
{
	//This gives the animator a random delay
	public class AnimatorRandomizeSpeed : UnityEngine.MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private RandomFloatRange _speedModifierRange = new RandomFloatRange(0.75f, 1.25f);
		private ILimitedRangeFloat speedModifierRange { get { return this._speedModifierRange; }}

	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Start ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }

			this.RandomizeAnimatorSpeed(this.animator);

			UnityEngine.Object.Destroy(this);
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		//delays this animator
		private void RandomizeAnimatorSpeed (Animator animator)
		{
			animator.speed *= this.speedModifierRange.random;
		}
	//ENDOF private methods
	}
}