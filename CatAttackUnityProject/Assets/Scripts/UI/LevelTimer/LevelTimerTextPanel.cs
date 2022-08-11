using UnityEngine;
using LevelTimer = CatAttack.LevelTimer;

using EUnlockableState = CatAttack.EUnlockableState;

using TextMeshProUGUI = TMPro.TextMeshProUGUI;

namespace CatAttack.UI
{
	public class LevelTimerTextPanel : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private TextMeshProUGUI textMesh;

		[SerializeField]
		private Animator animator;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Start()
		{
			this.UpdateDeployment();
		}

		private void Update ()
		{
			this.SetText(LevelManager.FormattedTime.toString);
		}
	//ENDOF MonoBehaviour

	//private properties
		private LevelTimer levelTimer { get { return LevelManager.LevelTimer; }}
	//ENDOF private properties

	//private methods
		private void UpdateDeployment ()
		{
			this.animator.SetBool("Hidden", UnlockablesManager.Stopwatch == EUnlockableState.Enabled);
		}

		private void SetText (string text)
		{
			this.textMesh.SetText(text);
		}
	//ENDOF private methods

	}
}