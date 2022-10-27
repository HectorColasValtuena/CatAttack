using UnityEngine;

namespace CatAttack
{
	public class CatProgrammedControlTrigger : MonoBehaviour, ICharacterInput
	{
	//serialized fields
		[SerializeField]
		private float horizontalAxis = 0f;
		[SerializeField]
		private bool jump = false;
	//ENDOF serialized fields

	//ICharacterInput implementation
		float ICharacterInput.horizontalAxis { get { return this.horizontalAxis; }}
		bool ICharacterInput.jump { get { return this.jump; }}
	//ENDOF ICharacterInput implementation
	}
}