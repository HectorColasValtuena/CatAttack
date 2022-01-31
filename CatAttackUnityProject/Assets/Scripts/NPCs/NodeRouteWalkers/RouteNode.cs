using UnityEngine;

namespace CatAttack.NodeRouteWalkers
{
	public class RouteNode :
		MonoBehaviour,
		IRouteNode
	{
	//IRouteNode implementation
		Transform IRouteNode.Transform { get { return this.transform; }}
	//ENDOF IRouteNode
	}
}