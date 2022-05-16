using UnityEngine;

using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;
using IFloatRange = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<float>;

namespace CatAttack.FX
{
	//Flags-up an animator trigger after given time has elapsed
	public class AnimatorTriggerOnTimeout : MonoBehaviour
	{
	//Serialized fields
		[Tooltip("Name of the trigger to set on timeout")]
		[SerializeField]
		private string triggerName = "triggerName";

		[Tooltip("Random range of time to trigger after")]
		[SerializeField]
		private RandomFloatRange _timeoutRange;
		private IFloatRange timeoutRange { get { return this._timeoutRange; }}

		[SerializeField]
		private Animator animator;
	//ENDOF Serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }
			this.timer = this.timeoutRange.random;
		}

		private void Update ()
		{
			if (this.timer <= 0)
			{
				this.Trigger();
				this.enabled = false;
			}
			else 
			{ this.timer -= Time.deltaTime; }
		}
	//ENDOF MonoBehaviour

	//Private fields
		private float timer;
	//ENDOF Private fields

	//Private methods
		private void Trigger ()
		{
			if (this.animator == null)
			{
				Debug.LogError(this.name + ".AnimatorTriggerOnTimeout.Trigger() no animator available");
				return;
			}

			this.animator.SetTrigger(this.triggerName);
		}
	//ENDOF Private methods
	}
}