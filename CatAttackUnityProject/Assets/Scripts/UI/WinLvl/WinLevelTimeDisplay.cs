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
		private bool isRecord
		{ get { return true; /*[TO-DO]*/ }}

		private bool timeAvailable
		{ get { return RunTimeAccumulator.FullRunAvailable; }}

		private string recordTimeString
		{ get { return new TimeFormatter(RunTimeAccumulator.FullRunCombinedTime).toString; }}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Start ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }

			this.animator.SetBool("RecordMode", this.isRecord);
			this.animator.SetBool("TimeAvailable", this.timeAvailable);

			if (this.timeAvailable)
			{
				this.timeField.Write(this.recordTimeString);
			}
		}
	//ENDOF MonoBehaviour lifecycle
	}
}