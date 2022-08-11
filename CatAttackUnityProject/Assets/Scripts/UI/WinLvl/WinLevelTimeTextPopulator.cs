using UnityEngine;

using TimeFormatter = CatAttack.Timing.TimeFormatter;

namespace CatAttack.Miscellaneous
{
	public class WinLevelTimeTextPopulator : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private UI.WorldTextLineInstantiator timeField;
	//ENDOF serialized fields	

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.timeField == null) { this.timeField = this.GetComponent<UI.WorldTextLineInstantiator>(); }
		}

		private void OnEnable ()
		{
			if (RunManager.FullRunAvailable)
			{ this.timeField.Write(this.SecondsToTimeString(this.RunTime)); }
		}
	//ENDOF MonoBehaviour

	//private properties
		private float RunTime
		{ get { return RunManager.RunCombinedTime; }}
	//ENDOF private properties

	//private methods
		//formats a time in seconds to an MM:ss.mmm
		private string SecondsToTimeString (float seconds)
		{ return new TimeFormatter(this.RunTime).toString; }
	//ENDOF private methods
	}
}