using UnityEngine;
namespace CatAttack
{
	public class SelfDestructOnStart : MonoBehaviour
	{
	//serialized fields
		[Tooltip("Delay in seconds between this object's first frame and its destruction")]
		[SerializeField]
		private float delay = 0;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Start ()
		{
			UnityEngine.Object.Destroy(this.gameObject, this.delay);
		}
	///ENDOF MonoBehaviour
	}
}