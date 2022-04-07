using UnityEngine;

namespace CatAttack
{
	[RequireComponent(typeof(SpriteRenderer))]

	[ExecuteAlways]
	public class SpriteSizeFromSpriteSize : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer originSpriteRenderer;

		[SerializeField]
		private bool replicateDepth = true;

		[SerializeField]
		private int depthOffset = -1;

		public void Awake ()
		{
			ResizeCollider();
		}

		public void ResizeCollider ()
		{
			
			SpriteRenderer targetSpriteRenderer = GetComponent<SpriteRenderer>();

			//if any required component is unavailable, report a log and exit
			if (targetSpriteRenderer == null || originSpriteRenderer == null) { Debug.Log("SpriteSizeFromSpriteSize required components not found!"); return; }

			//replicate sprite size
			targetSpriteRenderer.size = originSpriteRenderer.size;			

			//replicate depth
			if (replicateDepth)
			{ targetSpriteRenderer.sortingOrder = originSpriteRenderer.sortingOrder + depthOffset; }

			//self destruct
			if (Application.IsPlaying(this.gameObject))
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}
}