/*
*	interface representing a component that moves a gameobject
*/
namespace CatAttack.MovementControllers
{
	public interface IMovementController
	{
		bool enabled { get; set; }

		UnityEngine.Vector2 destination { set; }
		bool arrived { get; }
	}
}