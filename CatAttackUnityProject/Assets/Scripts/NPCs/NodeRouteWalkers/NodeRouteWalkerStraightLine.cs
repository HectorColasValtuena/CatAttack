using UnityEngine;

using static CatAttack.Extensions.Transform2DExtensions;	//Transform.ELookAt2D(target)

namespace CatAttack.NodeRouteWalkers
{
	//this is the simplest node route walker
	//moves in a straight line from node to node
	public class NodeRouteWalkerStraightLine :
		NodeRouteWalkerBase
	{}
}