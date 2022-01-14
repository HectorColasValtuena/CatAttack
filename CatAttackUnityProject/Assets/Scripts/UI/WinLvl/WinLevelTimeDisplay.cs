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
		{ get { return RunTimeAccumulator.FullRunCombinedTime; }}
		
		private bool IsRecord
		{
			get
			{
				if (!this.TimeAvailable) { return false; }
				return RecordVault.IsRecord(this.RunTime);
			}
		}

		private bool TimeAvailable
		{ get { return RunTimeAccumulator.FullRunAvailable; }}

		private string RecordTimeString
		{ get { return new TimeFormatter(this.RunTime).toString; }}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Start ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }

			this.animator.SetBool("RecordMode", this.IsRecord);
			this.animator.SetBool("TimeAvailable", this.TimeAvailable);

			if (this.TimeAvailable)
			{
				this.timeField.Write(this.RecordTimeString);
				RecordVault.RegisterTime(this.RunTime);
			}
		}
	//ENDOF MonoBehaviour lifecycle
	}
}