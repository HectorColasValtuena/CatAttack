using UnityEngine;

namespace CatAttack
{
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]

	public class ColliderSizeFromSprite : MonoBehaviour
	{
		private static Vector2 sizeToOffsetVector = new Vector2(0f, -0.5f);

		private BoxCollider2D targetBoxCollider2D;
		private Rigidbody2D targetRigidbody2D;
		private SpriteRenderer targetRspriteRenderer;

		public void Awake ()
		{
			ResizeCollider();
		}

		public void ResizeCollider ()
		{
			targetBoxCollider2D = GetComponent<BoxCollider2D>();
			targetRigidbody2D = GetComponent<Rigidbody2D>();
			targetRspriteRenderer = GetComponent<SpriteRenderer>();

			//if any required component is unavailable, report a log and exit
			if (targetBoxCollider2D == null || targetRigidbody2D == null || targetRspriteRenderer == null) { Debug.Log("ColliderSizeFromSprite required components not found!"); return; }

			//Debug.Log(targetRspriteRenderer.size);

			targetBoxCollider2D.offset = targetRspriteRenderer.size * sizeToOffsetVector;
			targetBoxCollider2D.size = targetRspriteRenderer.size;
		}
	}
}