using static PHATASS.Utils.Extensions.SpriteExtensions;

using UnityEngine;


namespace CatAttack
{
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]

	public class BoxColliderSizeFromSprite : MonoBehaviour
	{
	//const
		private static Vector2 sizeToOffsetVector = new Vector2(0f, -0.5f);
	//ENDOF const

	//serialized variables
		[SerializeField]
		[Tooltip("if true component will destroy itself after first update")]
		private bool selfDestruct = true;

		[Header("Margins")]
		[Tooltip("Negative values make the collider smaller than the sprite. Positive values make the collider larger than the sprite")]
		[SerializeField]
		private float marginTop = 0f;
		[Tooltip("Negative values make the collider smaller than the sprite. Positive values make the collider larger than the sprite")]
		[SerializeField]
		private float marginDown = 0f;
		[Tooltip("Negative values make the collider smaller than the sprite. Positive values make the collider larger than the sprite")]
		[SerializeField]
		private float marginLeft = 0f;
		[Tooltip("Negative values make the collider smaller than the sprite. Positive values make the collider larger than the sprite")]
		[SerializeField]
		private float marginRight = 0f;
	//ENDOF serialized

	//private variables
		private BoxCollider2D boxCollider2D;
		private Rigidbody2D rigidbody2D;
		private SpriteRenderer spriteRenderer;

		private Sprite lastSprite;
	//ENDOF private variables

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			this.ResizeCollider();
			this.TrySelfDestruct();
		}

		private void Update ()
		{
			if (this.spriteRenderer.sprite != this.lastSprite)
			{ this.ResizeCollider(); }
		}	
	//ENDOF MonoBehaviour

	//private properties
		private Vector2 spritePivot { get { return this.spriteRenderer.sprite.ENormalizedPivot(); }}

		private float verticalMargins { get { return this.marginTop + this.marginDown; }}
		private float horizontalMargins { get { return this.marginLeft + this.marginRight; }}

		private Vector2 colliderSize
		{
			get
			{
				Vector3 boundsSize = this.spriteRenderer.size;
				return new Vector2 (x: boundsSize.x + this.horizontalMargins, y: boundsSize.y + this.verticalMargins);
			}
		}

		private Vector2 offsetVector
		{
			get
			{
				return (this.spriteRenderer.size * (Vector2.one - this.spritePivot)) - (this.spriteRenderer.size / 2);
			}
		}

		private Vector2 marginOffsetCorrectionVector
		{
			get
			{
				return new Vector2(
					x: (this.marginRight - this.marginLeft) / 2,
					y: (this.marginTop - this.marginDown) / 2
				);
			}
		}
	//ENDOF properties

	//private methods
		private void ResizeCollider ()
		{
			this.boxCollider2D = this.GetComponent<BoxCollider2D>();
			this.rigidbody2D = this.GetComponent<Rigidbody2D>();
			this.spriteRenderer = this.GetComponent<SpriteRenderer>();

			//if any required component is unavailable, report a log and exit
			if (this.boxCollider2D == null || this.rigidbody2D == null || this.spriteRenderer == null)
			{ Debug.Log("ColliderSizeFromSprite required components not found!"); return; }

			//Sprite sprite 

			//Debug.Log(spriteRenderer.size);

			this.boxCollider2D.offset = this.offsetVector + this.marginOffsetCorrectionVector; //!!
			this.boxCollider2D.size = this.colliderSize;
		}

		private void TrySelfDestruct ()
		{
			if (this.selfDestruct) { UnityEngine.Object.Destroy(this); }
		}
	}
}