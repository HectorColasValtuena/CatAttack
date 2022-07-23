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
	// This movement controller flies in a wobbly line, divinding its flight in several segments of given time
	[RequireComponent(typeof(Rigidbody2D))]
	public class RigidbodySegmentedFlutterFlier :
		MonoBehaviour,
		IMovementController
	{
	//constants
	//ENDOF constants

	//serialized fields
		[Tooltip("Vertical force to apply when starting flight from previous target location")]
		[SerializeField]
		private RandomFloatRange _takeoffVerticalForce;
		private ILimitedRangeFloat takeoffVerticalForce { get { return this._takeoffVerticalForce; }}

		[Tooltip("Time between fluttering segments")]
		[SerializeField]
		private RandomFloatRange _flutterSegmentInterval;
		private ILimitedRangeFloat flutterSegmentInterval { get { return this._flutterSegmentInterval; }}

		[Tooltip("Maximum angular randomization given to each segment's ideal direction")]
		[SerializeField]
		private RandomFloatRange _angularDeviationRange;
		private ILimitedRangeFloat angularDeviationRange { get { return this._angularDeviationRange; }}

		[Tooltip("Force to apply at the start of each fluttering segment")]
		[SerializeField]
		private RandomFloatRange _flapForceRange;
		private ILimitedRangeFloat flapForceRange { get { return this._flapForceRange; }}

		[Tooltip("Continuous force applied each physics update while moving")]
		[SerializeField]
		private RandomFloatRange _flightContinuousForceRange;
		private ILimitedRangeFloat flightContinuousForceRange { get { return this._flightContinuousForceRange; }}

		[Tooltip("If false, continous force is generated once for each gameobject. If true, a new speed will be generated for each segment")]
		[SerializeField]
		private bool rerollForceEachSegment = false;

		[Tooltip("If false, flight direction is generated at the start of each segment. If true, flight direction is recalculated each update")]
		[SerializeField]
		private bool flightDirectionContinuousUpdate = false;

		[Tooltip("Flight closer than this distance to the target will diminish in force and direction randomization")]
		[SerializeField]
		private float closeApproximationDistance = 1f;

		[Tooltip("Minimum distance to targetPosition to consider we have arrived")]
		[SerializeField]
		private float minimumArrivalDistance = 0.1f;

		[Tooltip("Animator trigger name to set to true on each fluttering segment - none if empty")]
		[SerializeField]
		private string flapAnimationTriggerName = "Flap";

		[Tooltip("Animator bool name to set to true while flying")]
		[SerializeField]
		private string flightAnimationBoolName = "Flight";
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
			this.RandomizeContinuousFlightForce();
		}

		private void FixedUpdate ()
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
		private float modifierByDistance
		{ get {
			return //1 when on target, 0 at minimum distance or beyond
				(new RandomFloatRange(0f, this.closeApproximationDistance) as ILimitedRangeFloat)
				.ToNormalized(this.distanceToDestination);
		}}

		//vector representing currently desired flight direction
		/*private Angle2D flightDirection {
			get { return }
		}*/
	//ENDOF private properties

	//private fields
		private new Rigidbody2D rigidbody = null;
		private Animator animator = null;

		private Vector2? targetPosition = null;
		private Quaternion? targetRotation = null;

		private float segmentTimer = 0.0f;

		private float continuousFlightForce = 0.0f;
	//ENDOF private fields

	//private methods
		private void UpdateFlight ()
		{
			if (this.flying)
			{
				if (this.segmentTimer <= 0)
				{
					this.ResetFlightSegment();
				}
				this.segmentTimer -= Time.deltaTime;
			}
		}

		private void ResetFlightSegment ()
		{
			this.segmentTimer = this.flutterSegmentInterval.random;

		}

		private void Flap ()
		{
			if (this.animator != null)
			{ this.animator.SetTrigger(this.flapAnimationTriggerName); }

				//Vector2 forceVector = this.GetFlappingForceVector();
				//Debug.Log("Flap force vector: " + forceVector);
			this.rigidbody.AddForce(this.GetFlappingForceVector(), ForceMode2D.Impulse);
			
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

			float modifier = this.modifierByDistance;
			//Debug.Log("modifier: " + modifier);

			//calculate angle with a random deviation
			float angle = ((Vector2) this.transform.position).EFromToDegrees2D(destinationVector);
			angle += (this.angularDeviationRange.random * modifier);

			//transform degrees into a force and scale it with modifier and force
			Vector2 forceVector = angle.EDegreesToVector2() * modifier * this.flapForceRange.random;

			return forceVector;
		}

		//Caches a new random continuousFlightForce
		private void RandomizeContinuousFlightForce ()
		{ this.continuousFlightForce = this.flightContinuousForceRange.random; }
	//ENDOF private methods
	}
}