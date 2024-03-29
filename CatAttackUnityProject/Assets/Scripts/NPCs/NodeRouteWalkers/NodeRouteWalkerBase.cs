using UnityEngine;

using IMovementController = CatAttack.MovementControllers.IMovementController;

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
		private void Start ()
		{
			this.movementController = this.GetComponent<IMovementController>();
			
			this.UpdateActiveNode();

			if (this.routeNodes.Length == 0)
			{ this.SelfDisable(); }
		}

		private void Update ()
		{ 
			this.UpdateWalker();
		}
		private void UpdateWalker()
		{
			//check if we are over the target and for how long
			if (this.movementController.arrived) // this.activeNode.transform.position)
			{
				if (this.waitTimeElapsed >= this.activeNode.waitTime)
				{ this.StepActiveNode(); }
				else
				{ this.waitTimeElapsed += Time.deltaTime; }
			}
		}
	//ENDOF MonoBehaviour

	//private properties
		//returns currently active node
		private IRouteNode activeNode { get { return this.routeNodes[this.activeNodeIndex]; }}
	//ENDOF private properties

	//private fields
		private IMovementController movementController;

		//this is the counter for time elapsed waiting at a node. When reaching node wait time we move towards next route node
		private float waitTimeElapsed = 0f;
	//ENDOF private fields

	//private methods
		//steps to the next activeNodeIndex
		private void StepActiveNode ()
		{
			this.activeNodeIndex++;
			if (this.activeNodeIndex >= this.routeNodes.Length)
			{
				if (this.loop) { this.activeNodeIndex = 0; }
				else
				{
					this.SelfDisable();
					return;
				}
			}
			this.UpdateActiveNode();
		}

		//sets node to given target node
		private void UpdateActiveNode ()
		{
			this.waitTimeElapsed = 0.0f;
			if (this.activeNodeIndex >= this.routeNodes.Length)
			{
				this.movementController.targetPosition = null;
				this.movementController.targetRotation = null;
			}
			else 
			{
				this.movementController.targetPosition = this.activeNode.transform.position;
				this.movementController.targetRotation = this.activeNode.transform.rotation;
			}
		}

		//disables this node route walker
		private void SelfDisable ()
		{
			this.enabled = false;
		}
	//ENDOF private methods
	}
}