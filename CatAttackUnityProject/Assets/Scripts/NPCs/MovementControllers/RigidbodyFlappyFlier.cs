/*
*	This component moves containing rigidbody towards target
*	by giving impulses in random directions
*/

using UnityEngine;

using static PHATASS.Utils.Extensions.Vector2Extensions;

using ILimitedRangeFloat = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<float>;
using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;


namespace CatAttack.MovementControllers
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class RigidbodyFlappyFlier :
		MonoBehaviour,
		IMovementController
	{
	//constants
		private const float minimumArrivalDistance = 0.1f;
	//ENDOF constants

	//serialized fields
		[Tooltip("Time between flaps")]
		[SerializeField]
		private RandomFloatRange _flapInterval;
		private ILimitedRangeFloat flapInterval { get { return this._flapInterval; }}

		[Tooltip("Maximum angular randomization given to each flap")]
		[SerializeField]
		private RandomFloatRange _angularRandomRange;
		private ILimitedRangeFloat angularDeviationRange { get { return this._angularRandomRange; }}

		[Tooltip("Force to apply with each flap")]
		[SerializeField]
		private RandomFloatRange _flapForceRange;
		private ILimitedRangeFloat flapForceRange { get { return this._flapForceRange; }}

		[Tooltip("Flaps made closer than this distance to the target will diminish in force and randomization")]
		[SerializeField]
		private float closeApproximationDistance = 1f;

		[Tooltip("Animator trigger name to set to true on each flap - none if empty")]
		[SerializeField]
		private string flapAnimationTriggerName = "Flap";
	//ENDOF serialized fields

	//IMovementController implementation
		bool IMovementController.enabled
		{ set { this.enabled = value; } get { return this.enabled; }}

		Vector2? IMovementController.targetPosition
		{ set { this.targetPosition = value; }}

		Quaternion? IMovementController.targetRotation
		{ set { this.targetRotation = value; }}

		bool IMovementController.arrived
		{ get { return this.arrived; }}
	//ENDOF IMovementController

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			this.rigidbody = this.GetComponent<Rigidbody2D>();
			if (this.flapAnimationTriggerName != "") { this.animator = this.GetComponent<Animator>(); }
		}

		private void Update ()
		{
			this.UpdateFlight();
		}
	//ENDOF MonoBehaviour

	//private properties
		//true while this character is supposed to be in flight
		private bool flying 
		{ get { return !this.arrived && this.enabled; }}
		//returns true if this object is already at its destination
		private bool arrived
		{ get { return (this.targetPosition == null || this.distanceToDestination <= minimumArrivalDistance); }}

		//returns distance between this object and target node
		private float distanceToDestination
		{ get { return this.transform.position.EDistanceTo2D((Vector2) this.targetPosition); }}

		//returns a multiplier affecting flapping effects based on distance
		//when straight over the target return 0, at minimum distance or beyond return 1
		private float flapModifierByDistance
		{ get {
			float normalizedProximityMod = //1 when on target, 0 at minimum distance or beyond
				(new RandomFloatRange(0f, this.closeApproximationDistance) as ILimitedRangeFloat)
				.ToNormalized(this.distanceToDestination);

			return normalizedProximityMod;
		}}
	//ENDOF private properties

	//private fields
		private new Rigidbody2D rigidbody = null;
		private Animator animator = null;

		private Vector2? targetPosition = null;
		private Quaternion? targetRotation = null;

		private float flapTimer = 0.0f;
	//ENDOF private fields

	//private methods
		private void UpdateFlight ()
		{
			if (this.flying && this.flapTimer <= 0)
			{
				this.Flap();
			}
			else
			{ this.flapTimer -= Time.deltaTime; }
		}

		private void Flap ()
		{
			if (this.animator != null)
			{ this.animator.SetTrigger(this.flapAnimationTriggerName); }

				//Vector2 forceVector = this.GetFlappingForceVector();
				//Debug.Log("Flap force vector: " + forceVector);
			this.rigidbody.AddForce(this.GetFlappingForceVector(), ForceMode2D.Impulse);
			
			this.flapTimer = this.flapInterval.random;
		}

		//Creates a vector2 representing the desired forces for current flap
		private Vector2 GetFlappingForceVector ()
		{
			if (this.targetPosition == null)
			{
				//Debug.Log("RigidbodyFlappyFlier.GetFlappingForceVector() no target position");
				return Vector2.zero;
			}

			Vector2 destinationVector = (Vector2) this.targetPosition;

			float modifier = this.flapModifierByDistance;
			//Debug.Log("modifier: " + modifier);

			//calculate angle with a random deviation
			float angle = ((Vector2) this.transform.position).EFromToDegrees2D(destinationVector);
			angle += (this.angularDeviationRange.random * modifier);

			//transform degrees into a force and scale it with modifier and force
			Vector2 forceVector = angle.EDegreesToVector2() * modifier * this.flapForceRange.random;

			return forceVector;
		}
	//ENDOF private methods
	}
}