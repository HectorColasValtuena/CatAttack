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
		private ITypesetDictionary characterDictionary;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Start ()
		{
			this.Write(this.initialPhrase);
		}
	//ENDOF MonoBehaviour

	//public methods
		public void Write (string phrase)
		{
			this.Clear();
			float widthReached = 0f;

			//foreach
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
	}
}