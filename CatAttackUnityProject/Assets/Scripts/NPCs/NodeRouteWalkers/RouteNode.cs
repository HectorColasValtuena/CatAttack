using UnityEngine;

namespace CatAttack.NodeRouteWalkers
{
	public class RouteNode :
		MonoBehaviour,
		IRouteNode
	{
	//IRouteNode implementation
		Transform IRouteNode.transform { get { return this.transform; }}

		float IRouteNode.waitTime { get { return this.waitTime; }}
		[SerializeField]
		private float waitTime = 0f;
	//ENDOF IRouteNode
	}
}