using UnityEngine;

using static CatAttack.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)

namespace CatAttack.NodeRouteWalkers
{
	//this is the base for NodeRouteWalkers
	//they move this gameobject's transform position through a series of IRouteNode (ckeckpoins)
	public abstract class NodeRouteWalkerBase :
		MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private float speed;

		[SerializeField]
		private RouteNode[] routeNodes;

		[SerializeField]
		private bool loop = true;

		[SerializeField]
		private int activeNodeIndex = 0;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Update ()
		{ this.UpdateWalker(); }
		private void UpdateWalker()
		{
			//check if we are over the target and for how long
			if (this.transform.position == this.activeNode.transform.position)
			{
				if (this.waitTimeElapsed >= this.activeNode.waitTime)
				{ this.StepActiveNode(); }
				else
				{ this.waitTimeElapsed += Time.deltaTime; }
			}
			//otherwise continue moving
			else
			{
				this.Move();
			}
		}
	//ENDOF MonoBehaviour

	//private properties
		//returns currently active node
		private IRouteNode activeNode { get { return this.routeNodes[this.activeNodeIndex]; }}

		//returns distance between this object and target node
		private float distanceToNode { get { return (this.transform.position - this.activeNode.transform.position).magnitude; }}

		//returns maximum movement for current frame
		private float frameMovement { get { return this.speed * Time.deltaTime; }}
	//ENDOF private properties

	//private fields
		//this is the counter for time elapsed waiting at a node. When reaching node wait time we move towards next route node
		private float waitTimeElapsed = 0f;
	//ENDOF private fields

	//private methods
		//steps to the next activeNodeIndex
		private void StepActiveNode ()
		{
			this.waitTimeElapsed = 0.0f;
			this.activeNodeIndex++;
			if (this.activeNodeIndex >= this.routeNodes.Length)
			{
				if (this.loop) { this.activeNodeIndex = 0; }
				else { this.activeNodeIndex = this.routeNodes.Length - 1; }
			}
		}

		//move the character towards next target
		private void Move ()
		{
			if (this.distanceToNode > this.frameMovement)
			{ this.MoveTowardsNode(); }
			else
			{
				this.CopyNodePosition();
				this.OnNodeReached();
			}
		}

		protected virtual void MoveTowardsNode ()
		{
			this.transform.ELookAt2D(this.activeNode.transform);
			this.transform.Translate(x: this.frameMovement, y: 0, z: 0);
		}

		private void CopyNodePosition ()
		{
			this.transform.rotation = this.activeNode.transform.rotation;
			this.transform.position = this.activeNode.transform.position;
		}

		protected virtual void OnNodeReached () {}
	//ENDOF private methods
	}
}