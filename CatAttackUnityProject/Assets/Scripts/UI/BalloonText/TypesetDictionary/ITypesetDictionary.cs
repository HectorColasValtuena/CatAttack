using RectTransform = UnityEngine.RectTransform;

namespace CatAttack.UI
{
    //dictionary handling a set of characters - can be requested individual characters
    public interface ITypesetDictionary
    {
        //gets the prefab corresponding to substring (by its RectTransform)
        ITypesetCharacterSample GetSample (char character);
    }
}