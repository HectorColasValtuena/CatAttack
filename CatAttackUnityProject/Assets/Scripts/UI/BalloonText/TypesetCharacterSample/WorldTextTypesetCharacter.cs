using UnityEngine;

namespace CatAttack.UI
{
	//controls a gameobject character (letter or number)
	[RequireComponent(typeof(RectTransform))]
	public class WorldTextTypesetCharacter : MonoBehaviour, ITypesetCharacterSample
	{
	//ITypesetCharacterSample implementation
		//character or substring represented by this character
		char ITypesetCharacterSample.character { get { return this.characterRepresented; }}
		[SerializeField]
		public char characterRepresented;

		//returns width in units
		float ITypesetCharacterSample.width 
		{
			get
			{
				RectTransform rectTransform = (this.transform as RectTransform);
				if (rectTransform == null) { return 0f; }
				return rectTransform.rect.width;
			}
		}
	//ENDOF ITypesetCharacterSample
	}
}