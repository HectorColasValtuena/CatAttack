using UnityEngine;

namespace CatAttack
{
	public class DeleteGamePanelController : MonoBehaviour
	{
		const float escButtonHoldTimeRequired = 2.0f;
		const float autoCloseTime = 4.0f;

		private float escButtonTimer = 0.0f;
		private float deployedTimer = 0.0f;
		private bool deployed = false;

		private Animator animator;

		private bool EscIsPressed
		{
			get
			{
				return UnityEngine.Input.GetKey(KeyCode.Escape);
			}
		}

		public void Awake ()
		{
			animator = GetComponent<Animator>();
			SetDeployed(false);
		}

		public void Update ()
		{
			if (!deployed)
			{
				if (EscIsPressed) { escButtonTimer += Time.deltaTime; }
				else { escButtonTimer = 0.0f; }

				if( escButtonTimer > escButtonHoldTimeRequired)
				SetDeployed(true);
			}
			else
			{
				deployedTimer += Time.deltaTime;
				if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || deployedTimer >= autoCloseTime)
				{ SetDeployed(false); }
			}
		}

		public void DoDeleteSaveGame ()
		{
			ProgressionManager.ResetSave();
			this.Hide();
		}

		public void DoHide ()
		{
			this.Hide();
		}

		private void SetDeployed (bool status)
		{
			deployedTimer = 0.0f;
			escButtonTimer = 0.0f;
			animator.SetBool("Deployed", status);
			deployed = status;
		}


		private void Deploy ()
		{
			this.SetDeployed(true);
		}

		private void Hide ()
		{
			this.SetDeployed(false);
		}
	}
}