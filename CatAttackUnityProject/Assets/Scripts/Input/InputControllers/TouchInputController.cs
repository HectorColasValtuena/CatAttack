using UnityEngine;

namespace CatAttack.Input
{
	public class TouchInputController :
		MonoBehaviour,
		IInputController
	{
	//serialized fields
		[SerializeField]
		private RectTouchButton _leftWalkButton;
		private IButton leftWalkButton { get { return this._leftWalkButton; }}

		[SerializeField]
		private RectTouchButton _rightWalkButton;
		private IButton rightWalkButton { get { return this._rightWalkButton; }}

		[SerializeField]
		private RectTouchButton _leftJumpButton;
		private IButton leftJumpButton { get { return this._leftJumpButton; }}

		[SerializeField]
		private RectTouchButton _rightJumpButton;
		private IButton rightJumpButton { get { return this._rightJumpButton; }}
	//ENDOF serialized fields

	//IInputController implementation
		bool IInputController.jump { get { return this.jump; }}
		float IInputController.horizontalAxis { get { return this.horizontalAxis; }}
	//ENDOF IInputController implementation

	//private properties
		private bool jumpInput
		{ get { return this.rightJumpButton.pressed || this.leftJumpButton.pressed; }}

		private bool leftInput
		{ get { return this.leftWalkButton.pressed || this.leftJumpButton.pressed; }}
		
		private bool rightInput
		{ get { return this.rightWalkButton.pressed || this.rightJumpButton.pressed; }}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		private void Update ()
		{
			this.jump = this.jumpInput;
			this.horizontalAxis = 0.0f;

			//further check only if both sides are not on the same state (both pressed or both up)
			if (this.leftInput != this.rightInput)
			{
				if (this.leftInput)
				{ this.horizontalAxis = -1f; }
				else
				{ this.horizontalAxis = 1f; }
			}
		}
	//ENDOF MonoBehaviour lifecycle

	//private fields
		private bool jump = false;
		private float horizontalAxis = 0f;
	//ENDOF private fields
	}
}