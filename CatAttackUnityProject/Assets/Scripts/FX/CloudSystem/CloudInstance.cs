﻿using UnityEngine;

namespace CatAttack
{
	public class CloudInstance : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;

		public Vector2 moveSpeed;
		public Transform centerPoint = null;
		public Sprite cloudSprite { set { spriteRenderer.sprite = value; } }

		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			//flip roughly half of the cloud sprites on creation
			spriteRenderer.flipX = Random.Range(0,2) == 1;
		}

		void Update()
		{
			//ensure we have a center point
			if (centerPoint == null) { centerPoint = transform.parent; }

			//move the cloud
			transform.Translate(new Vector3 (moveSpeed.x * Time.deltaTime, moveSpeed.y * Time.deltaTime, 0));

			//check our position relative to the centre point - if out of range ask manager for a position reset
			if
			(
				transform.position.x > centerPoint.position.x + CloudManager.instance.maxCloudDistance.x ||
				transform.position.x < centerPoint.position.x - CloudManager.instance.maxCloudDistance.x ||
				transform.position.y > centerPoint.position.y + CloudManager.instance.maxCloudDistance.y ||
				transform.position.y < centerPoint.position.y - CloudManager.instance.maxCloudDistance.y
			) {
				CloudManager.instance.ResetCloud(this);
			}
		}
	}
}