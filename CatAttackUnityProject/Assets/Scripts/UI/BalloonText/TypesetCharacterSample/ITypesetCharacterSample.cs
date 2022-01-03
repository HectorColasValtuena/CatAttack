namespace CatAttack.UI
{
	public interface ITypesetCharacterSample
	{
		char character { get; } //character or substring represented by this character
		float width { get; }		//returns width in world units
	}
}