namespace CatAttack
{
	//interface representing a single quest - either full or a step of one
	public interface IQuest
	{
		//wether this quest element is completed
		bool Completed { get; }
	}
}