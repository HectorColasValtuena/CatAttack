using UnityEngine;

namespace CatAttack.FX
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteFlipFromRigidbodyMomentum : MonoBehaviour
	{
	//Serialized fields
		[Tooltip("If true, sprite's X axis will be flipped with the rigidbody's velocity X component sign")]
		[SerializeField]
		private bool horizontalAxis = true;

		[Tooltip("If true, sprite's Y axis will be flipped with the rigidbody's velocity Y component sign")]
		[SerializeField]
		private bool verticalAxis = false;		
	//ENDOF Serialized

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			this.rigidbody = this.GetComponent<Rigidbody2D>();
			this.spriteRenderer = this.GetComponent<SpriteRenderer>();
		}

		public void LateUpdate ()
		{
			Vector2 velocity = this.rigidbody.velocity;
			if (this.horizontalAxis) { this.DoHorizontal(velocity.x); }
			if (this.verticalAxis) { this.DoVertical(velocity.y); }
		}
	//ENDOF MonoBehaviour

	//private fields
		private new Rigidbody2D rigidbody;
		private SpriteRenderer spriteRenderer;
	//ENDOF private fields

	//private methods
		private void DoHorizontal (float axisValue)
		{
			if (axisValue == 0) { return; }
			if (axisValue < 0) { this.spriteRenderer.flipX = true; }
			else { this.spriteRenderer.flipX = false; }
		}

		private void DoVertical (float axisValue)
		{
			if (axisValue == 0) { return; }
			if (axisValue < 0) { this.spriteRenderer.flipY = true; }
			else { this.spriteRenderer.flipY = false; }
		}
	//ENDOF private methods
	}
}