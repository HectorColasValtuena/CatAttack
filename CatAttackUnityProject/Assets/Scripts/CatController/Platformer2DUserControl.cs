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
		private bool m_Jump;


		private void Awake()
		{
			Platformer2DUserControl.playerCatRigidbody = gameObject.GetComponent<Rigidbody2D>();
			m_Character = GetComponent<PlatformerCharacter2D>();
		}


		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}


		private void FixedUpdate()
		{
			// Read the inputs.
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			// Pass all parameters to the character control script.
			m_Character.Move(h, crouch, m_Jump);
			m_Jump = false;
		}
	}
}
