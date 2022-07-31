using UnityEngine;

namespace CatAttack.FX
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class ImitateParentSpriteFlip : MonoBehaviour
	{
	//serialized fields
		[Tooltip("This is the sprite from which we will imitate our own flip. If empty will try to get the spriteRenderer from the parent hierarchy.")]
		[SerializeField]
		private SpriteRenderer parentSpriteRenderer;

		[Tooltip("Our spriteRenderer")]
		[SerializeField]
		private SpriteRenderer spriteRenderer;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			if (this.spriteRenderer == null) { this.spriteRenderer = this.GetComponent<SpriteRenderer>(); }
			if (this.parentSpriteRenderer == null) { this.parentSpriteRenderer = this.gameObject.transform.parent.gameObject.GetComponentInParent<SpriteRenderer>(); }
		}

		private void LateUpdate ()
		{
			this.spriteRenderer.flipX = this.parentSpriteRenderer.flipX;
			this.spriteRenderer.flipY = this.parentSpriteRenderer.flipY;
		}
	//ENDOF MonoBehaviour lifecycle

	//private fields
	//ENDOF private fields
	}
}
