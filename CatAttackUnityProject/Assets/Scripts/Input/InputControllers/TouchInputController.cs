using UnityEngine;

namespace CatAttack.Input
{
	public class TouchInputController :
		MonoBehaviour,
		IInputController
	{
	//IInputController implementation
		bool IInputController.jump { get { return false; }}
		float IInputController.horizontalAxis { get { return 0f; }}
	//ENDOF IInputController implementation
	}
}