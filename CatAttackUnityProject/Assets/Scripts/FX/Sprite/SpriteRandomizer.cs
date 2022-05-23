using System.Collections.Generic;
using UnityEngine;

using static PHATASS.Utils.Extensions.IListExtensions;
using static PHATASS.Utils.RandomUtils.RandomBoolUtils;

namespace CatAttack.FX
{
	public class SpriteRandomizer : MonoBehaviour
	{
	//Serialized fields
		[Tooltip("List of possible sprites. On awake, one of them will be set on the target renderer.")]
		[SerializeField]
		private Sprite[] _spriteList;
		private IList<Sprite> spriteList { get { return this._spriteList; }}

		[Tooltip("If true, also randomize sprite X flip")]
		[SerializeField]
		private bool randomizeFlipX = false;

		[Tooltip("If true, Sprite will be re-randomized on OnEnable")]
		[SerializeField]
		private bool refreshOnEnable = false;

		[Tooltip("Target sprite renderer. If none, will get this object's sprite renderer")]
		[SerializeField]
		private SpriteRenderer spriteRenderer;
	//ENDOF Serialized

	//MonoBehaviour Lifecycle
		private void Awake ()
		{
			if (this.spriteRenderer == null) { this.spriteRenderer = this.GetComponent<SpriteRenderer>(); }
			
			if (!this.refreshOnEnable) { this.RandomizeSprite(); }
			if (!this.refreshOnEnable) { UnityEngine.Object.Destroy(this); }
		}

		private void OnEnable ()
		{
			this.RandomizeSprite();
		}
	//ENDOF MonoBehaviour Lifecycle


	//Private methods
		private void RandomizeSprite ()
		{
			if (this.spriteList == null || this.spriteList.Count < 1) { Debug.LogError(this.gameObject.name + ".RandomizeSprite: Sprite list empty or none"); return; }
			this.spriteRenderer.sprite = this.spriteList.ERandomElement<Sprite>();

			if (this.randomizeFlipX) { this.spriteRenderer.flipX = randomBool; }
		}
	//ENDOF Private methods
	}
}