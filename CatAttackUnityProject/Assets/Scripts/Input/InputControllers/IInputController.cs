namespace CatAttack.Input
{
	public interface IInputController
	{
		//a boolean representing if jump input is pressed/held
		bool jump { get; }

		//a float ranging from -1 (left) to +1 (right) representing current horizontal axis input. Neutral/none is 0.
		float horizontalAxisInput { get; }
	}
}