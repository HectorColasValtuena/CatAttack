/*
*	Controls a critter that stands in one in various resting positions.
*	Once player character enters detection trigger, critter enters scared state
*	and flees towards a random safe spot, for a while.
*
*	Requires a trigger to detect player character and an IMovementController component
*/

using UnityEngine;

namespace CatAttack.Critters
{
	public class ScaredyCritterController
	{
	//serialized fields
		//list of transforms representing the various resting places the critter will stay when idle
		[SerializeField]
		private Transform[] restingPlaces;

		//list of transforms representing the points the critter will flee towards when scared
		[SerializeField]
		private Transform[] safePlaces;

		//time to stay scared after no longer detecting the player
		[SerializeField]
		private RandomFloatRange scaredTime;
	//ENDOF serialized
	}
}