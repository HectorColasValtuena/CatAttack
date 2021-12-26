using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CatAttack
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
		public static Rigidbody2D playerCatRigidbody;

		private PlatformerCharacter2D m_Character;

		private void Awake()
		{
			Platformer2DUserControl.playerCatRigidbody = gameObject.GetComponent<Rigidbody2D>();
			m_Character = GetComponent<PlatformerCharacter2D>();
		}

		private void FixedUpdate()
		{
			// Read the inputs.
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			bool jump = CrossPlatformInputManager.GetButton("Jump");
			//bool jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);

			// Pass all parameters to the character control script.
			m_Character.Move(h, jump);
		}
	}
}
