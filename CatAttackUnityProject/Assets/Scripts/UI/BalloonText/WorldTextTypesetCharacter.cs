using UnityEngine;

namespace CatAttack.UI
{
	//controls a gameobject character (letter or number)
	public class WorldTextTypesetCharacter : MonoBehaviour
	{
	//public properties
		//character or substring represented by this character
		[SerializeField]
		public string characterRepresented;

		//returns width in units
		public float characterWidth 
		{
			get
			{
				RectTransform rectTransform = (this.transform as RectTransform);
				if (rectTransform == null) { return 0f; }
				return rectTransform.rect.width;
			}
		}
	//ENDOF public properties
	}
}