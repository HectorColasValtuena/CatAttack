using UnityEngine;

namespace CatAttack.NodeRouteWalkers
{
	public class RouteNode :
		MonoBehaviour,
		IRouteNode
	{
	//IRouteNode implementation
		Transform IRouteNode.Transform { get { return this.transform; }}

		float IRouteNode.WaitTime { get { return this.waitTime; }}
		[SerializeField]
		private float waitTime = 0f;
	//ENDOF IRouteNode
	}
}