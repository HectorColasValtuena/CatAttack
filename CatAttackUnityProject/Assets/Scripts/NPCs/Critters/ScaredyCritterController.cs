/*
*	Controls a critter that stands in one in various resting positions.
*	Once player character enters detection trigger, critter enters scared state
*	and flees towards a random safe spot, for a while.
*
*	Requires a trigger to detect player character and an IMovementController component
*/

using UnityEngine;

using static PHATASS.Utils.Extensions.IListExtensions;

using ILimitedRangeFloat = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<float>;
using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;

using IMovementController = CatAttack.MovementControllers.IMovementController;

namespace CatAttack.Critters
{
	public class ScaredyCritterController : MonoBehaviour
	{
	//serialized fields
		[Tooltip("list of transforms representing the various resting places the critter will stay when idle")]
		[SerializeField]
		private Transform[] restingPlaces;

		[Tooltip("list of transforms representing the points the critter will flee towards when scared")]
		[SerializeField]
		private Transform[] safePlaces;

		[Tooltip("Time to stay scared after no longer detecting the player")]
		[SerializeField]
		private RandomFloatRange _scaredTimeRange;
		private ILimitedRangeFloat scaredTimeRange { get { return this._scaredTimeRange; }}
	//ENDOF serialized

	//private fields
		private IMovementController movementController;

		//remaining time scared
		private float scaredTimer = 0.0f;
	//ENDOF private fields

	//MonoBehaviour
		private void Awake ()
		{
			this.movementController = this.GetComponent<IMovementController>();
		}

		private void Start ()
		{
			//ensure a target is chosen at the start
			this.NewTarget();
		}

		private void Update ()
		{
			if (this.isScared)
			{
				//update scared timer if scared
				this.scaredTimer -= Time.deltaTime;

				//if no longer scared anymore, or arrived at current target, generate new destination
				if (!this.isScared || this.movementController.arrived)
				{
					this.NewTarget();
				}
			}
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			Debug.Log("" + other.gameObject.name + " scared me!");
			this.Scare();
		}
	//ENDOF MonoBehaviour

	//private properties
		private bool isScared
		{ get { return this.scaredTimer >= 0; }}
	//ENDOF private properties

	//private methods
		private void Scare ()
		{
			this.scaredTimer = this.scaredTimeRange.random;
			this.NewTarget();
		}

		private void NewTarget ()
		{
			Transform[] candidates;

			if (this.isScared)
			{ candidates = this.safePlaces; }
			else 
			{ candidates = this.restingPlaces; }

			this.SetTarget(candidates.ERandomElement<Transform>());
		}

		private void SetTarget (Transform target)
		{
			this.movementController.targetPosition = target.position;
			this.movementController.targetRotation = target.rotation;
		}
	//ENDOF private methods
	}
}