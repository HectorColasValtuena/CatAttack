using UnityEngine;

namespace CatAttack.Triggers
{
	//trigger that spawns an object or set of objects when triggered
	public class InstantiateOnTrigger :
		MonoBehaviour,
		ITrigger
	{
	//serialized field
		[SerializeField]
		GameObject[] instantiatePrefabList;

		[SerializeField]
		Transform spawnSpot;

		[SerializeField]
		Transform spawnParent;
	//ENDOF serialized

	//ITrigger implementation
		void ITrigger.Trigger ()
		{ this.Trigger(); }
		public void Trigger ()
		{
			if (this.spawnSpot == null) { this.spawnSpot = this.transform; }
			foreach (GameObject prefab in this.instantiatePrefabList)
			{
				UnityEngine.Object.Instantiate(
					original: prefab,
					position: spawnSpot.position,
					rotation: spawnSpot.rotation,
					parent: this.spawnParent
				);
			}
		}
	//ENDOF ITrigger
	}
}