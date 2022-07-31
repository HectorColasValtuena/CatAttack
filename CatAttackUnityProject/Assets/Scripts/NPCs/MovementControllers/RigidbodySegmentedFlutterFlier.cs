/*
*	This component moves containing rigidbody towards target
*	by giving impulses in random directions
*/

using UnityEngine;

using static PHATASS.Utils.Extensions.Vector2Extensions;

using ILimitedRangeFloat = PHATASS.Utils.Types.Ranges.ILimitedRange<float>;
using RandomFloatRange = PHATASS.Utils.Types.Ranges.RandomFloatRange;

using IAngle2D = PHATASS.Utils.Types.IAngle2D;
using static PHATASS.Utils.Types.IAngle2DFactory;


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

		[Tooltip("If true, when very near the target this object will be snapped to target and its forces reset")]
		[SerializeField]
		private bool snapToDestination = false;

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
			this.animator = this.GetComponent<Animator>();
			this.RandomizeContinuousFlightForce();

			if (this.animator != null && this.flightAnimationBoolName != "")
			{ this.animator.SetBool(this.flightAnimationBoolName, true); }
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

		//returns this transform's position, as a Vector2
		private Vector2 transformPosition
		{ get { return (Vector2) this.transform.position; }}

		//returns a multiplier affecting flapping effects based on distance
		//when straight over the target return 0, at minimum distance or beyond return 1
		private float modifierByDistance
		{ get {
			return
				(new RandomFloatRange(0f, this.closeApproximationDistance) as ILimitedRangeFloat)
				.ToNormalized(this.distanceToDestination);
		}}

		//vector representing currently desired flight direction
		private IAngle2D flightDirection
		{ get { return this.desiredFlightDirection + this.flightAngularDeviation; }}

		private Vector2 flightDirectionVector
		{ get { return this.flightDirection.EAngle2DToVector2(); }}


		//returns angular deviation, scaled by modifierByDistance
		private IAngle2D flightAngularDeviation
		{
			get { return 0f.AsDegrees().Lerp(this._flightAngularDeviation, this.modifierByDistance); }
			set { this._flightAngularDeviation = value; }
		}
		private IAngle2D _flightAngularDeviation;
	//ENDOF private properties

	//private fields
		private new Rigidbody2D rigidbody = null;
		private Animator animator = null;

		private Vector2? targetPosition = null;
		private Quaternion? targetRotation = null;

		private float segmentTimer = 0.0f;

		private float continuousFlightForce = 0.0f;

		private IAngle2D desiredFlightDirection;

		private bool wasInFlight = true;
	//ENDOF private fields

	//private methods
		private void UpdateFlight ()
		{
			if (this.flying)
			{
				if (!this.wasInFlight) { this.TakeOff(); }

				if (this.segmentTimer <= 0)	{ this.ResetFlightSegment(); }
				else if (this.flightDirectionContinuousUpdate || this.distanceToDestination < this.closeApproximationDistance)
				{ this.UpdateDesiredFlightDirection(); }

				this.ApplyContinuousFlightForce();


				this.segmentTimer -= Time.deltaTime;
			}
			else
			{
				if (this.wasInFlight) { this.Landing(); }
				this.TrySnap();
			}

			this.wasInFlight = this.flying;
		}

		private void TakeOff ()
		{
			this.segmentTimer = 0;
			this.rigidbody.AddForce(new Vector2(0, this.takeoffVerticalForce.random), ForceMode2D.Impulse);

			if (this.animator != null && this.flightAnimationBoolName !="")
			{ this.animator.SetBool(this.flightAnimationBoolName, true); }
		}

		private void Landing ()
		{
			if (this.animator != null && this.flightAnimationBoolName !="")
			{ this.animator.SetBool(this.flightAnimationBoolName, false); }
		}

		private void ResetFlightSegment ()
		{
			this.segmentTimer = this.flutterSegmentInterval.random;

			if (this.rerollForceEachSegment) { this.RandomizeContinuousFlightForce(); }

			this.UpdateDesiredFlightDirection();
			this.RandomizeFlightDeviation();

			this.Flap();
		}

		private void UpdateDesiredFlightDirection ()
		{
			if (this.targetPosition == null) { return; }

			this.desiredFlightDirection = this.transformPosition.EFromToAngle2D((Vector2) this.targetPosition);
		}

		private void RandomizeFlightDeviation ()
		{
			this.flightAngularDeviation = this.angularDeviationRange.random.AsDegrees();
		}

		private void Flap ()
		{
			if (this.animator != null && this.flapAnimationTriggerName != "")
			{ this.animator.SetTrigger(this.flapAnimationTriggerName); }

			this.rigidbody.AddForce(
				force: this.flightDirectionVector * this.flapForceRange.random * this.modifierByDistance,
				mode: ForceMode2D.Impulse
			);
		}

		private void ApplyContinuousFlightForce ()
		{
			this.rigidbody.AddForce(
				force: this.flightDirectionVector * this.continuousFlightForce * (0.5f + 0.5f * this.modifierByDistance),
				mode: ForceMode2D.Force
			);
		}

		//Caches a new random continuousFlightForce
		private void RandomizeContinuousFlightForce ()
		{ this.continuousFlightForce = this.flightContinuousForceRange.random; }

		//tries to snap to destination position if we are close enough
		private void TrySnap ()
		{
			if (!this.snapToDestination || this.targetPosition == null) { return; }

			if (this.distanceToDestination < this.minimumArrivalDistance)
			{
				this.rigidbody.position = (Vector2) this.targetPosition;
				this.rigidbody.velocity = Vector2.zero;
			}
		}
	//ENDOF private methods
	}
}