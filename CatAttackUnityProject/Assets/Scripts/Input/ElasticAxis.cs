using SerializeField = UnityEngine.SerializeField;

using IValueMutableFloat = PHATASS.Utils.Math.IValueMutable<float>;

using RandomFloatRange = PHATASS.Utils.Math.Ranges.RandomFloatRange;
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
		private void UpdateAxis ()
		{
		//	this.
		}
	//ENDOF private methods
	}
}