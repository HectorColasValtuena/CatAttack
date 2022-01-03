using System.Collections.Generic;
using UnityEngine;

namespace CatAttack.UI
{
	public class GameObjectTypesetDictionary : MonoBehaviour, ITypesetDictionary
	{
	//serialized fields
		[SerializeField]
		private WorldTextTypesetCharacter[] characterList;

		[SerializeField]
		private WorldTextTypesetCharacter nullCharacter;
	//ENDOF serialized fields

	//ITypesetDictionary implementation
		ITypesetCharacterSample ITypesetDictionary.GetSample (char character)
		{ return this.GetSample(character); }
	//ENDOF ITypesetDictionary

	//private fields
		private IDictionary<char, ITypesetCharacterSample> characterCache;
	//ENDOF private fields

	//private methods
		void ValidateCache ()
		{
			if (this.characterCache == null)
			{ this.InitCache(); }
		}
		void InitCache ()
		{
			this.characterCache = new Dictionary<char, ITypesetCharacterSample>();
			foreach (ITypesetCharacterSample entry in characterList)
			{
				this.characterCache.Add(entry.character, entry);
			}
		}

		ITypesetCharacterSample GetSample (char character)
		{
			this.ValidateCache();

			if (this.characterCache.ContainsKey(character))
			{ return this.characterCache[character]; }
			return this.nullCharacter;
		}
	//ENDOF private methods
	}
}