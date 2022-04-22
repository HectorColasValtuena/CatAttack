/*
* Movement controller that moves in a straight line ignoring any environment
*/

using static PHATASS.Utils.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)
using static PHATASS.Utils.Extensions.Vector2Extensions;

//Unity attributes
using TooltipAttribute = UnityEngine.TooltipAttribute;
using SerializeField = UnityEngine.SerializeField;

//Unity types
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

namespace CatAttack.MovementControllers
{

	public class TransformFlierStraightLine :
		UnityEngine.MonoBehaviour,
		IMovementController
	{
	//serialized properties
		[Tooltip("Movement in world units per second")]
		[SerializeField]
		private float speed = 1.0f;

		[Tooltip("If true this character's right vector will always look towards destination")]
		[SerializeField]
		private bool lookAtDestination = true;
	//ENDOF serialized

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
		private void Update ()
		{
			this.Move();
		}
	//ENDOF MonoBehaviour

	//private fields
		private Vector2? targetPosition = null;

		private Quaternion? targetRotation = null;
	//ENDOF private fields

	//private properties
		//returns distance between this object and target node
		private float distanceToDestination
		{ get { return this.transform.position.EDistanceTo2D((Vector2) this.targetPosition); }}

		//returns maximum movement for current frame
		private float frameSpeed
		{ get { return this.speed * UnityEngine.Time.deltaTime; }}

		//returns true if this object is already at its destination
		private bool arrived
		{ get { return (this.targetPosition == null || this.distanceToDestination <= this.frameSpeed); }}
	//ENDOF private properties

	//private methods
		//move the character towards target
		private void Move ()
		{
			if (this.arrived)
			{
				this.SetDestinationPosition();
				this.OnDestinationReached(); 
			}
			else { this.MoveTowardsDestination(); }
		}

		protected virtual void MoveTowardsDestination ()
		{
			if (this.targetPosition == null) { return; }
			if (this.lookAtDestination) { this.transform.ELookAt2D((Vector2) this.targetPosition); }
			this.transform.EMoveTowards2D((Vector2) this.targetPosition, this.frameSpeed);
		}

		private void SetDestinationPosition ()
		{
			if (this.targetRotation != null) { this.transform.rotation = (Quaternion) this.targetRotation; }
			if (this.targetPosition != null) { this.transform.position = (Vector2) this.targetPosition; }
		}

		//Shall I remove you?
		protected virtual void OnDestinationReached () {}
	//ENDOF private methods
	}
}