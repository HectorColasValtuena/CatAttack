using Math = System.Math;

namespace CatAttack
{
	public class TimeFormatter
	{
	//constructor
		public TimeFormatter (double _seconds)
		{ this.secondsAndMilliseconds = _seconds; }
	//ENDOF constructor

	//public properties
		public string toString
		{ get { return string.Format("{0:D1}:{1:D2}.{2:D3}", this.minutes, this.seconds, this.milliseconds); }}

		public int minutes
		{ get { return (int) Math.Floor((double) (this.trimmedSeconds / 60)); }}

		public int seconds
		{ get { return this.trimmedSeconds - (this.minutes * 60); }}
		
		public int milliseconds
		{
			get 
			{
				double decimals = this.secondsAndMilliseconds - this.trimmedSeconds;
				UnityEngine.Debug.Log(string.Format("Decimals: {0}", decimals));
				return (int) Math.Round(decimals * 1000);
			}
		}
	//ENDOF public properties

	//private properties
		//raw amount of seconds in this time, not converting any of them to minutes but trimming milliseconds
		private int trimmedSeconds
		{ get { return (int) Math.Floor(this.secondsAndMilliseconds); }}
	//ENDOF private properties
	
	//private fields
		private double secondsAndMilliseconds;
	//ENDOF private fields
	}
}