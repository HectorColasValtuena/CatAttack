using Time = UnityEngine.Time;

using SerializeField = UnityEngine.SerializeField;

using static PHATASS.Utils.MathUtils.FloatExtensions;

using IValueMutableFloat = PHATASS.Utils.MathUtils.IValueMutable<float>;

using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;
using IUpdatable = CatAttack.Interfaces.IUpdatable;

namespace CatAttack.Input
{
	public class ElasticAxis :
		RandomFloatRange,
		IValueMutableFloat,
		IUpdatable
	{
	//serialized fields
		[SerializeField] private float changeSpeed = 3.0f;
		[SerializeField] private float deadZone = 0.001f;
		[SerializeField] private bool snap = true;
	//ENDOF serialized fields

	//IUpdatable implementation
		void IUpdatable.Update ()
		{
			this.UpdateAxis();
		}
	//ENDOF IUpdatable

	//IValueMutableFloat
		float IValueMutableFloat.value { set { this.SetValue(value); }}
	//ENDOF IValueMutableFloat

	//method overrides
		protected override float GetValue ()
		{
	
			if (this.value < this.deadZone && this.value > (-this.deadZone))
			{ return 0; }
			return this.value;
		}

		private void SetValue (float value)
		{ this.targetValue = UnityEngine.Mathf.Clamp(value, -1f, 1f); }
	//ENDOF method overrides

	//constructor
		public ElasticAxis () : base(minimum: -1f, maximum: 1f) {}
	//ENDOF constructor

	//private fields
		private float targetValue = 0f;
		private float value = 0f;
	//ENDOF private fields

	//private methods
		//on update move axis value towards target value
		private void UpdateAxis ()
		{
			//if snap is enabled and signs are opposite, snap value back to zero
			if (this.snap && (this.value.ESign() == (this.targetValue.ESign() * -1)))
			{ this.value = 0.0f; }

			//now change value accordingly
			this.value = this.value.EStepTowards(this.targetValue, this.changeSpeed * Time.deltaTime);
		}
	//ENDOF private methods
	}
}