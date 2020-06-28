using UnityEngine;

namespace CatAttack.UI
{
	public class PredictiveCameraController : MonoBehaviour
	{
		//private Transform target;
		private Camera myCamera;

		public Vector2 maxAdvanceRate = new Vector2(1.0f, 0.5f);
		public Vector2 maxAdvanceDistance = new Vector2(1.0f, 0.5f);
		public float lerpRate = 0.1f;

		public float minSize = 1f;
		public float sizePerSpeed = 0.1f;
		public float sizeLerpRate = 0.25f;
		public float maxSize = 2f;


		void Awake ()
		{
			myCamera = GetComponent<Camera>();
			//target = transform.parent.GetComponent<PlatformerCharacter2D>().m_CameraTarget;
			//transform.SetParent(null);
		}

		void Update ()
		{
			//predict player characters future position and move the camera ahead of the player
			if (Platformer2DUserControl.playerCatRigidbody == null) { Debug.LogWarning("PredictiveCameraController.Update(): No player catracter"); return; }
			Vector3 relativeTargetPosition = new Vector3
			(
				CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.x*maxAdvanceRate.x, maxAdvanceDistance.x),
				CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.y*maxAdvanceRate.y, maxAdvanceDistance.y),
				-10
			);

			//lerp the position towards the target
			transform.localPosition = Vector3.Lerp(transform.localPosition, relativeTargetPosition, lerpRate);

			//adjust size
			float targetSize = minSize + (sizePerSpeed * Platformer2DUserControl.playerCatRigidbody.velocity.magnitude);
			myCamera.orthographicSize = (Mathf.Lerp(
				myCamera.orthographicSize,
				(targetSize < maxSize) ? targetSize : maxSize,
				sizeLerpRate
			));
		}

		private float CapValue (float value, float cap)
		{
			if (Mathf.Abs(value) > Mathf.Abs(cap))
			{
				return Mathf.Abs(cap)*Mathf.Sign(value);
			}
			return value;
		}
	}
}