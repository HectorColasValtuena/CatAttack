namespace CatAttack.Input
{
	public class AnalogInputController : IInputController
	{
	//IInputController implementation
		bool IInputController.jump { get { return UnityEngine.Input.GetButton("Jump"); }}
		float IInputController.horizontalAxis { get { return UnityEngine.Input.GetAxis("Horizontal"); }}
	//ENDOF IInputController
	}
}