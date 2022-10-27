using UnityEngine;

using InputControllerCache = CatAttack.Input.InputControllerCache;

namespace CatAttack
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class CatControlSchemeUserInput : CatControlSchemeBase
	{
	//private properties
		//is touch input enabled
		private bool touchInputEnabled { get { return InputControllerCache.touchInputEnabled; }}

		//input from analog sources (keyboard/joysticks)
		private float analogInputHorizontal { get { return InputControllerCache.analogInputController.horizontalAxis; }}
		private bool analogInputJump { get { return InputControllerCache.analogInputController.jump; }}

		//input from touch screen
		private float touchInputHorizontal { get { return InputControllerCache.touchInputController.horizontalAxis; }}
		private bool touchInputJump { get { return InputControllerCache.touchInputController.jump; }}
	//ENDOF private properties

	//property overrides
		//final input getters
		protected override float horizontalInput
		{
			get
			{
				if (this.touchInputEnabled)
				{ return this.analogInputHorizontal + this.touchInputHorizontal; }
				return this.analogInputHorizontal;
			}
		}
		protected override bool jumpInput
		{
			get
			{
				if (this.touchInputEnabled)
				{ return this.analogInputJump || this.touchInputJump; }
				return this.analogInputJump;
			}
		}
	//ENDOF property overrides
	}
}
