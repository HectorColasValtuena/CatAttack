/*
* Movement controller that moves in a straight line ignoring any environment
*/

using static CatAttack.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)

using SerializeField = UnityEngine.SerializeField;
using Vector2 = UnityEngine.Vector2;

namespace CatAttack.MovementControllers
{

	public class TransformFlierStraightLine :
		UnityEngine.MonoBehaviour,
		IMovementController
	{
	//serialized properties
		[SerializeField]
		private float speed = 1.0f;
	//ENDOF serialized

	//IMovementController implementation
		Vector2 IMovementController.destination
		{ set { this.destination = value; }}
		private Vector2 destination;

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
		//returns distance between this object and target node
		private float distanceToDestination
		{ get { return (((Vector2) this.transform.position) - this.destination).magnitude; }}

		//returns maximum movement for current frame
		private float frameMovement
		{ get { return this.speed * UnityEngine.Time.deltaTime; }}

		//returns true if this object is already at its destination
		private bool arrived
		{ get { return (this.distanceToDestination <= this.frameMovement); }}
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
			this.transform.ELookAt2D(this.destination);
			this.transform.Translate(x: this.frameMovement, y: 0, z: 0);
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