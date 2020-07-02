using UnityEngine;

namespace CatAttack
{
	public class CloudInstance : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;

		[System.NonSerialized] public float moveSpeed;
		public Sprite cloudSprite { set { spriteRenderer.sprite = value; } }

		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			//flip roughly half of the cloud sprites on creation
			spriteRenderer.flipX = Random.Range(0,2) == 1;
		}

		void Update()
		{
			//move the cloud
			transform.Translate(new Vector3 (moveSpeed * Time.deltaTime, 0, 0));

			//check our position relative to the parent - if out of range ask manager for a position reset
			if
			(
				transform.localPosition.x > CloudManager.instance.maxCloudDistance.x ||
				transform.localPosition.x < -CloudManager.instance.maxCloudDistance.x ||
				transform.localPosition.y > CloudManager.instance.maxCloudDistance.y ||
				transform.localPosition.y < -CloudManager.instance.maxCloudDistance.y
			){
				CloudManager.instance.ResetCloud(this);
			}
		}
	}
}