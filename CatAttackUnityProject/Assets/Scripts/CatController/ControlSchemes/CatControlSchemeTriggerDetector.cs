using UnityEngine;

using InputControllerCache = CatAttack.Input.InputControllerCache;

namespace CatAttack
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class CatControlSchemeTriggerDetector : CatControlSchemeBase
	{
	//property overrides
		//final input getters
		protected override float horizontalInput { get { return this.horizontalAxis; }}
		protected override bool jumpInput {	get { return this.jump; }}
	//ENDOF property overrides

	//private fields
		private float horizontalAxis = 0f;
		private bool jump = false;
	//ENDOF private fields

	//MonoBehaviour lifecycle
		private void OnTriggerEnter2D (Collider2D other) { this.Triggered(other); }
		private void OnTriggerStay2D (Collider2D other) { this.Triggered(other); }
		private void Triggered (Collider2D other)
		{
			ICharacterInput inputs = other.GetComponent<CatProgrammedControlTrigger>();
			if (inputs != null) { this.InputsReceived(inputs.jump, inputs.horizontalAxis); }
		}

	//ENDOF MonoBehaviour lifecycle

	//method overrides
		protected override void LateFixedUpdate ()
		{
			this.ResetInputs();
		}
	//ENDOF method overrides

	//private methods
		private void InputsReceived (bool jumpReceived, float horizontalAxisReceived)
		{
			this.jump = jumpReceived || this.jump;
			this.horizontalAxis = Mathf.Max(horizontalAxisReceived, this.horizontalAxis);
		}

		private void ResetInputs ()
		{
			this.horizontalAxis = 0f;
			this.jump = false;
		}
	//ENDOF private methods
	}
}
