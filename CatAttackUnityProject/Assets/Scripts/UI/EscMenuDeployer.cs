using UnityEngine;

namespace CatAttack
{
	public class EscMenuDeployer : MonoBehaviour
	{
		private Animator animator;

		void Awake ()
		{
			animator = GetComponent<Animator>();
		}

		void Update()
		{
			if (UnityEngine.Input.GetKeyDown("escape"))
			{
				ToggleMenu();
			}
		}

		public void ToggleMenu ()
		{
			animator.SetBool("IsDeployed", !animator.GetBool("IsDeployed"));
		}

		public void QuitButtonPressed ()
		{
			ProgressionManager.LevelChange(1);  //TO-DO move this to a constant somewhere safe
		}
	}
}