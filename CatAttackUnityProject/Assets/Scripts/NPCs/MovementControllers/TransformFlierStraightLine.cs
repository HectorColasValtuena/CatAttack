/*
* Movement controller that moves in a straight line ignoring any environment
*/

using static PHATASS.Utils.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)

using SerializeField = UnityEngine.SerializeField;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

namespace CatAttack.MovementControllers
{

	public class TransformFlierStraightLine :
		UnityEngine.MonoBehaviour,
		IMovementController
	{
	//serialized properties
		[SerializeField]
		private bool lookAtDestination = true;

		[SerializeField]
		private float speed = 1.0f;
	//ENDOF serialized

	//IMovementController implementation
		bool IMovementController.enabled
		{ set { this.enabled = value; } get { return this.enabled; }}

		Vector2? IMovementController.targetPosition
		{ set { this.targetPosition = value; }}

		Quaternion? IMovementController.targetRotation
		{ set { this.targetRotation; }}

		bool IMovementController.arrived
		{ get { return this.arrived; }}
	//ENDOF IMovementController

	//MonoBehaviour lifecycle
		private void Update ()
		{
			this.Move();
		}
	//ENDOF MonoBehaviour

	//private properties
		//desired position
		private Vector2? targetPosition = null;

		//returns distance between this object and target node
		private float distanceToDestination
		{ get { return (((Vector2) this.transform.position) - this.destination).magnitude; }}

		//returns maximum movement for current frame
		private float frameSpeed
		{ get { return this.speed * UnityEngine.Time.deltaTime; }}

		//returns true if this object is already at its destination
		private bool arrived
		{ get { return (this.distanceToDestination <= this.frameSpeed); }}
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
			if (this.lookAtDestination) { this.transform.ELookAt2D(this.destination); }
			this.transform.EMoveTowards2D(this.destination, this.frameSpeed);
		}

		private void SetDestinationPosition ()
		{
			//this.transform.rotation = this.activeNode.transform.rotation;
			this.transform.position = this.destination;
		}

		protected virtual void OnDestinationReached () {}
	//ENDOF private methods
	}
}