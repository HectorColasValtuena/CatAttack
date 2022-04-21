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
	//serialized fields
		[Tooltip("Time between flaps")]
		[SerializeField]
		private RandomFloatRange _flapInterval;
		private ILimitedRangeFloat flapInterval { get { return this._flapInterval; }}

		[Tooltip("Maximum angular randomization given to each flap")]
		[SerializeField]
		private RandomFloatRange _angularRandomRange;
		private ILimitedRangeFloat angularRandomRange { get { return this._angularRandomRange; }}

		[Tooltip("Force to apply with each flap")]
		[SerializeField]
		private RandomFloatRange _flapForceRange;
		private ILimitedRangeFloat flapForceRange { get { return this._flapForceRange; }}

		[Tooltip("Flaps made closer than this distance to the target will diminish in force and randomization")]
		private float closeApproximationDistance = 0.2f;
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
		}

		private void Update ()
		{
			
		}
	//ENDOF MonoBehaviour

	//private fields
		private new Rigidbody2D rigidbody;

		private Vector2? targetPosition = null;
		private Quaternion? targetRotation = null;
	//ENDOF private fields

	//private properties
		//returns true if this object is already at its destination
		private bool arrived
		{ get { return (this.targetPosition == null || this.distanceToDestination <= this.closeApproximationDistance); }}

		//returns distance between this object and target node
		private float distanceToDestination
		{ get { return this.transform.position.EDistanceTo2D((Vector2) this.targetPosition); }}
	//ENDOF private properties
	}
}