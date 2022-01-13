using UnityEngine;

namespace CatAttack.UI
{
	//this class is capable of instantiating a phrase made out of WorldObjectTypesetCharacter onto a single
	public class WorldTextLineInstantiator : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private string initialPhrase;

		[SerializeField]
		private RectTransform container;

		[SerializeField]
		private GameObjectTypesetDictionary serializedDictionary;
		private ITypesetDictionary characterDictionary { get { return (ITypesetDictionary) this.serializedDictionary; }}
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.container == null)
			{ this.container = this.transform as RectTransform; }

			if (this.initialPhrase != "")
			{ this.Write(this.initialPhrase); }
		}
	//ENDOF MonoBehaviour

	//public methods
		public void Write (string phrase, bool clear = true)
		{
			if (clear) { this.Clear(); }

			float widthReached = 0f;

			foreach (char character in phrase.ToCharArray())
			{
				widthReached = this.CreateCharacterAt(character, widthReached);
			}

			this.container.sizeDelta = new Vector2(x: widthReached, y: this.container.sizeDelta.y);
		}

		//removes every child within the container
		public void Clear ()
		{
			foreach (RectTransform child in this.container)
			{
				UnityEngine.Object.Destroy(child.gameObject);
			}
			this.container.sizeDelta = new Vector2(x: 0f, y: this.container.sizeDelta.y);
		}
	//ENDOF public methods

	//private methods
		//creates a character at xOffset horizontal offset. returns new total xOffset (summing previous and new character's)
		private float CreateCharacterAt (char character, float xOffset)
		{
			ITypesetCharacterSample sample = this.characterDictionary.GetSample(character);
			GameObject newObject = UnityEngine.Object.Instantiate(
				original: sample.gameObject,
				parent: this.container
			);
			(newObject.transform as RectTransform).anchoredPosition = new Vector2(x: xOffset, y: 0f);
			return xOffset + sample.width;
		}
	//ENDOF private methods
	}
}