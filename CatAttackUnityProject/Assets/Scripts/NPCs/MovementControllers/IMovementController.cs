/*
*	interface representing a component that moves a gameobject
*/
namespace CatAttack.MovementControllers
{
	public interface IMovementController
	{
		UnityEngine.Vector2 destination { set; }
		bool arrived { get; }
	}
}