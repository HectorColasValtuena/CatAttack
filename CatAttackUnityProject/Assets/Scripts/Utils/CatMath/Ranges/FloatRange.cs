using TRangeType = System.Single;

namespace CatAttack.CatMath.Ranges
{
	//floating point numeric range subclass
	[System.Serializable]
	public class FloatRange : BaseNumericRange<TRangeType>
	{
	//constructor
		public FloatRange (TRangeType min, TRangeType max) : base(min, max){}
	//ENDOF constructor

	//abstract property implementation
		public override TRangeType random
		{
			get	{ return UnityEngine.Random.Range(minimum, maximum); }
		}

		public override TRangeType difference
		{
			get { return maximum - minimum; }
		}
	//ENDOF abstract property implementation

	//abstract method implementation
		protected override TRangeType FromNormal (float normal)
		{
			return minimum + (difference * normal);
		}

		//returns a normalized (0 to 1) from a value within the range, without any consideration for value clamping
		protected override float ToNormal (TRangeType value)
		{
			return (value - minimum) / difference;
		}
	//ENDOF abstract method implementation
	}
}