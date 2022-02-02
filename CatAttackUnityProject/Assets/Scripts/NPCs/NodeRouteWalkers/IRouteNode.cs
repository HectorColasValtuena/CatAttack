using Transform = UnityEngine.Transform;

namespace CatAttack.NodeRouteWalkers
{
	public interface IRouteNode
	{
		Transform Transform { get; }
		float WaitTime { get; }
	}
}