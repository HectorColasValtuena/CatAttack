using SerializeField = UnityEngine.SerializeField;

namespace CatAttack.CatMath.Ranges
{
	//Base class defining a numeric range - with a minimum, maximum, and interpolated values
	[System.Serializable]
	public abstract class BaseNumericRange <TRangeType>
		: ILimitedRangeMutable<TRangeType>
	/*
	where TRangeType :
		System.IComparable<TRangeType>//,
		//System.IEquatable<TRangeType>
	/**/
	{
	//constructor
		public BaseNumericRange (TRangeType min, TRangeType max)
		{
			minimum = min;
			maximum = max;
		}
	//ENDOF constructor

	//ILimitedRange & ILimitedRangeMutable implementation
		//min and max values of the range
		public TRangeType minimum
		{
			get { return _minimum; }
			set { _minimum = value; }
		}
		public TRangeType maximum
		{
			get { return _maximum; }
			set { _maximum = value; }
		}

		//get a random value within defined range
		public abstract TRangeType random { get; }
		//{ get { return UnityEngine.Random.Range(minimum, maximum); }}

		//difference between maximum and minimum values
		public abstract TRangeType difference { get; }
		//{ get { return maximum - minimum; }}

		//generate a number within the range from a normalized (0 to 1) value. value will be clamped within minimum and maximum unless unclamped = true
		public TRangeType FromNormalized (float normalized, bool clamped = true)
		{
			if (clamped)
			{ normalized = UnityEngine.Mathf.Clamp(value: normalized, min: 0f, max: 1f); }

			return FromNormal(normalized);
		}

		//get a normalized value (0 to 1) from a numeric value within the range. value will be clamped within minimum and maximum unless clamped = false
		public float ToNormalized (TRangeType value, bool clamped = true)
		{
			float normal = ToNormal(value);

			if (clamped)
			{ normal = UnityEngine.Mathf.Clamp(value: normal, min: 0f, max: 1f); }

			return normal;
		}
	//ENDOF ILimitedRange & ILimitedRangeMutable 

	//private properties
	//ENDOF private properties

	//private fields
		[SerializeField]
		private TRangeType _minimum;

		[SerializeField]
		private TRangeType _maximum;
	//ENDOF private fields

	//private methods
		//returns value within the range from a 0 to 1 propotion, without any consideration for value clamping
		protected abstract TRangeType FromNormal (float normal); //return minimum + (difference * normalized);

		//returns a normalized (0 to 1) from a value within the range, without any consideration for value clamping
		protected abstract float ToNormal (TRangeType value);
	//ENDOF private methods
	}
}
