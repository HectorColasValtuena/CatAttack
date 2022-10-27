using System;
using UnityEngine;

using InputControllerCache = CatAttack.Input.InputControllerCache;

using IUpdatable = CatAttack.Interfaces.IUpdatable;
using IValueMutableFloat = PHATASS.Utils.Types.IValueMutable<float>;
using IValueFloat = PHATASS.Utils.Types.IValue<float>;

namespace CatAttack
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public abstract class CatControlSchemeBase : MonoBehaviour
	{
	//private fields
		//character controller cache
		private PlatformerCharacter2D character;

		//horizontal input axis
		private CatAttack.Input.ElasticAxis hInputAxis;
		private IValueMutableFloat horizontalAxis { get { return this.hInputAxis; }}
		private IUpdatable updatableHorizontalAxis { get { return this.hInputAxis; }}
	//ENDOF private fields

	//abstract properties
		//input getters
		protected abstract float horizontalInput { get; }
		protected abstract bool jumpInput { get; }
	//ENDOF abstract properties

	//MonoBehaviour lifecycle
		private void Start ()
		{
			this.character = GetComponent<PlatformerCharacter2D>();
			//initialize our movement axis 
			this.hInputAxis = new CatAttack.Input.ElasticAxis();
		}

		private void FixedUpdate ()
		{
			this.horizontalAxis.value = this.horizontalInput;
			this.updatableHorizontalAxis.Update();

			// Pass all parameters to the character control script.
			this.character.Move((this.horizontalAxis as IValueFloat).value, this.jumpInput);

			this.LateFixedUpdate();
		}
	//ENDOF MonoBehaviour lifecycle

	//abstract methods
		protected virtual void LateFixedUpdate () {}
	//ENDOF abstract methods
	}
}
