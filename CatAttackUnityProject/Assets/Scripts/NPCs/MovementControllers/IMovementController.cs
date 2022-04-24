/*
*	interface representing a component that moves a gameobject
*/
namespace CatAttack.MovementControllers
{
	public interface IMovementController
	{
		bool enabled { get; set; }						// Enable/disable this movement controller

		UnityEngine.Vector2? targetPosition { set; }	// Position to move to. NULL = free movement (none/idle)
		UnityEngine.Quaternion? targetRotation { set; }	// Desired rotation AT THE END of the movement. NULL = free rotation

		bool arrived { get; }							// Returns true if desired position is reached

	//implement me maybe?
	//	void Jump (float forceFactor = 1.0f);			// Jump, modifying default jump force/speed by forceFactor
	}
}