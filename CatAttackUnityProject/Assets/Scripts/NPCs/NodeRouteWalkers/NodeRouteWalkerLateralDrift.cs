using UnityEngine;

using static CatAttack.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)

using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;
using IFloatRange = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<System.Single>;


namespace CatAttack.NodeRouteWalkers
{
	//this is the simplest node route walker
	//moves in a straight line from node to node
	public class NodeRouteWalkerLateralDrift :
		NodeRouteWalkerBase
	{
	//serialized fields
		[SerializeField]
		private RandomFloatRange _lateralImpulseRange;
		private IFloatRange lateralImpulseRange { get { return this._lateralImpulseRange; }}

		[SerializeField]
		private float lateralDecelerationRate;
	//ENDOF serialized

	//private fields
		private float lateralVelocity;
	//ENDOF private fields

	//private properties
		private float lateralFrameMovement { get { return this.lateralVelocity * Time.deltaTime; }}
	//ENDOF private properties

	//overrides
	/*
		protected override void MoveTowardsNode ()
		{
			base.MoveTowardsNode();

			this.transform.Translate(x: 0, y: this.lateralFrameMovement, z: 0);
			this.lateralVelocity *= 1 - this.lateralDecelerationRate;
		}

		protected override void OnNodeReached ()
		{
			//base.OnNodeReached();
			this.lateralVelocity = this.lateralImpulseRange.random;
		}
	*/
	//ENDOF overrides

	//private methods
	//ENDOF private methods
	}
}