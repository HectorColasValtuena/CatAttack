using UnityEngine;

namespace CatAttack.Miscellaneous
{
	public class WinLevelTimeDisplay : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private Animator animator;
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

		private bool FullRun
		{ get { return RunManager.FullRunAvailable; }}

		private bool AllSecrets
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//[TO-DO]
		{ get { return false ; }}
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			if (this.animator == null) { this.animator = this.GetComponent<Animator>(); }

			this.animator.SetBool("Record", this.IsRecord);
			this.animator.SetBool("FullRun", this.FullRun);
			this.animator.SetBool("PerfectRun", this.AllSecrets);

			if (this.FullRun)
			{
				RecordVault.RegisterTime(this.RunTime);
			}
			/* //previous record is no longer displayed in this level
			else if (RecordVault.HasRecord)
			{
				this.timeField.Write(this.SecondsToTimeString(RecordVault.Record));
			}
			*/
		}
	//ENDOF MonoBehaviour lifecycle
	}
}