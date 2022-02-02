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
		private float speed;

		[SerializeField]
		private RouteNode[] routeNodes;

		[SerializeField]
		private bool loop = false;

		[SerializeField]
		private int activeNodeIndex = 0;
	//ENDOF serialized

	//MonoBehaviour lifecycle
		private void Update ()
		{ this.UpdateWalker(); }
		private void UpdateWalker()
		{
			//check if we are over the target and for how long
			if (this.transform.position == ActiveNode.Transform.position)
			{
				if (this.waitTimeElapsed >= ActiveNode.WaitTime)
				{ this.StepActiveNode(); }
				else
				{ this.waitTimeElapsed += Time.deltaTime; }
			}
			//otherwise continue moving
			else
			{
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			}
		}
	//ENDOF MonoBehaviour

	//private properties
		private IRouteNode ActiveNode { get { return this.routeNodes[this.activeNodeIndex]; }}
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