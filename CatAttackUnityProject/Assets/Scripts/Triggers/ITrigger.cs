namespace CatAttack.Triggers
{
	//interface representing a trigger-able component
	//does something relevant when calling the Trigger() method
	interface ITrigger
	{
		void Trigger ();
	}
}