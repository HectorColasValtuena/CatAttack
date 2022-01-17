using UnityEngine;

namespace CatAttack
{
	public class WinLevelTimeDisplay : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Animator animator;

		[SerializeField]
		private UI.WorldTextLineInstantiator timeField;
	//ENDOF serialized fields		

	//private properties
		private float RunTime
		{ get { return RunManager.FullRunCombinedTime; }}
		
		private bool IsRecord
		{
			get
			{
				if (!RunManager.FullRunAvailable) { return false; }
				return RecordVault.IsRecord(this.RunTime);
			}
		}

		private bool TimeAvailable
		{ get { return RunManager.FullRunAvailable || RecordVault.HasRecord; }}

	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Start ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }

			this.animator.SetBool("RecordMode", this.IsRecord);
			this.animator.SetBool("TimeAvailable", this.TimeAvailable);

			if (this.TimeAvailable)
			{
				this.timeField.Write(this.SecondsToTimeString(this.RunTime));
				RecordVault.RegisterTime(this.RunTime);
			}
			else if (RecordVault.HasRecord)
			{
				this.timeField.Write(this.SecondsToTimeString(RecordVault.Record));
			}
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		//formats a time in seconds to an MM:ss.mmm
		private string SecondsToTimeString (float seconds)
		{ return new TimeFormatter(this.RunTime).toString; }
	//ENDOF private methods
	}
}