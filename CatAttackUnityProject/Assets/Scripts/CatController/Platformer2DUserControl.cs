using System;
using UnityEngine;

using InputControllerCache = CatAttack.Input.InputControllerCache;

using IUpdatable = CatAttack.Interfaces.IUpdatable;
using IValueMutableFloat = PHATASS.Utils.MathUtils.IValueMutable<float>;
using IValueFloat = PHATASS.Utils.MathUtils.IValue<float>;

namespace CatAttack
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
	//private fields
		//character controller cache
		private PlatformerCharacter2D character;

		//horizontal input axis
		private CatAttack.Input.ElasticAxis hInputAxis;
		private IValueMutableFloat horizontalAxis { get { return this.hInputAxis; }}
		private IUpdatable updatableHorizontalAxis { get { return this.hInputAxis; }}
	//ENDOF private fields

	//private properties
		//is touch input enabled
		private bool touchInputEnabled { get { return InputControllerCache.touchInputEnabled; }}

		//input from analog sources (keyboard/joysticks)
		private float analogInputHorizontal { get { return InputControllerCache.analogInputController.horizontalAxis; }}
		private bool analogInputJump { get { return InputControllerCache.analogInputController.jump; }}

		//input from touch screen
		private float touchInputHorizontal { get { return InputControllerCache.touchInputController.horizontalAxis; }}
		private bool touchInputJump { get { return InputControllerCache.touchInputController.jump; }}

		//final input getters
		private float horizontalInput
		{
			get
			{
				if (this.touchInputEnabled)
				{ return this.analogInputHorizontal + this.touchInputHorizontal; }
				return this.analogInputHorizontal;
			}
		}
		private bool jumpInput
		{
			get
			{
				if (this.touchInputEnabled)
				{ return this.analogInputJump || this.touchInputJump; }
				return this.analogInputJump;
			}
		}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		private void Start ()
		{
			this.character = GetComponent<PlatformerCharacter2D>();
			//initialize our movement axis 
			this.hInputAxis = new CatAttack.Input.ElasticAxis();
		}

		private void FixedUpdate()
		{
			this.horizontalAxis.value = this.horizontalInput;
			this.updatableHorizontalAxis.Update();

			// Pass all parameters to the character control script.
			this.character.Move((this.horizontalAxis as IValueFloat).value, this.jumpInput);
		}
	//MonoBehaviour lifecycle
	}
}
