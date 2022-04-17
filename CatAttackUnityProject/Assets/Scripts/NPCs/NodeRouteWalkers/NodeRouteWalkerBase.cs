using UnityEngine;

namespace CatAttack.NodeRouteWalkers
{
	//this is the base for NodeRouteWalkers
	//they move this gameobject's transform position through a series of IRouteNode (ckeckpoins)
	public abstract class NodeRouteWalkerBase :
		MonoBehaviour
	{
	//serialized fields
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
	//ENDOF private methods
	}
}