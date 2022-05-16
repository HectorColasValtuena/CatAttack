using UnityEngine;

namespace CatAttack.Triggers
{
	//trigger that destroys an object when triggered
	public class DestroyOnTrigger :
		MonoBehaviour,
		ITrigger
	{
	//serialized field
		[Tooltip("List of objects that will be destroyed on trigger")]
		[SerializeField]
		private GameObject[] destroyTargetList;

		[Tooltip("Delay for the destruction of listed objects")]
		[SerializeField]
		private float delay;		
	//ENDOF serialized

	//ITrigger implementation
		void ITrigger.Trigger ()
		{ this.Trigger(); }
		public void Trigger ()
		{
			foreach (GameObject target in this.destroyTargetList)
			{
				UnityEngine.Object.Destroy(target, this.delay);
			}
		}
	//ENDOF ITrigger
	}
}