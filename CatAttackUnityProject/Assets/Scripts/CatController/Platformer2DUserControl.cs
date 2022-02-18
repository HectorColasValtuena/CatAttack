using System;
using UnityEngine;

using IUpdatable = CatAttack.Interfaces.IUpdatable;
using IValueMutableFloat = PHATASS.Utils.Math.IValueMutable<float>;
using IValueFloat = PHATASS.Utils.Math.IValue<float>;

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

		//is touch input enabled
		private bool touchInputEnabled;
	//ENDOF private fields

	//private properties
		//input from analog sources (keyboard/joysticks)
		private float analogInputHorizontal { get { return UnityEngine.Input.GetAxis("Horizontal"); }}
		private bool analogInputJump { get { return UnityEngine.Input.GetButton("Jump"); }}


		//input from touch screen
		private float touchInputHorizontal { get { return 0; }}
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		private bool touchInputJump { get { return false; }}


		//input getters return 
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
		private void Awake()
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
