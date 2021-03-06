﻿using UnityEngine;

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
			if (Input.GetKeyDown("escape"))
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
			ProgressionManager.LevelChange(0);
		}
	}
}