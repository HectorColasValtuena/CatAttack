using Transform = UnityEngine.Transform;

namespace CatAttack.NodeRouteWalkers
{
	public interface IRouteNode
	{
		Transform transform { get; }
		float waitTime { get; }
	}
}