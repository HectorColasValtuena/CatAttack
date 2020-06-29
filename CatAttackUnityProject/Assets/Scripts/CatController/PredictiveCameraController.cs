using UnityEngine;

namespace CatAttack.UI
{
	public class PredictiveCameraController : MonoBehaviour
	{
		private Vector3 alternativeFocusPosition = new Vector3(0, 0, -10);

		//private Transform target;
		private Camera myCamera;

		public Vector2 maxAdvanceRate = new Vector2(1.0f, 0.5f);
		public Vector2 maxAdvanceDistance = new Vector2(1.0f, 0.5f);
		public float lerpRate = 0.1f;

		public float minSize = 1f;
		public float sizePerSpeed = 0.1f;
		public float sizeLerpRate = 0.25f;
		public float maxSize = 2f;

		public float focusedSize = 1f;

		void Awake ()
		{
			myCamera = GetComponent<Camera>();
			//target = transform.parent.GetComponent<PlatformerCharacter2D>().m_CameraTarget;
			//transform.SetParent(null);
		}

		void Update ()
		{
			if (Platformer2DUserControl.playerCatRigidbody == null) { Debug.LogWarning("PredictiveCameraController.Update(): No player catracter"); return; }

			Vector3 relativeTargetPosition;
			float targetSize;

			//if a camera focus target is set, get its position. Otherwise follow the player.
			if (LevelManager.cameraFocusTarget != null) 
			{
				relativeTargetPosition = alternativeFocusPosition;
				targetSize = focusedSize;
			}
			else 
			{
				//predict player character future position and move the camera ahead of the player
				relativeTargetPosition = new Vector3
				(
					CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.x*maxAdvanceRate.x, maxAdvanceDistance.x),
					CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.y*maxAdvanceRate.y, maxAdvanceDistance.y),
					-10
				);
				//calculate size per speed
				targetSize = minSize + (sizePerSpeed * Platformer2DUserControl.playerCatRigidbody.velocity.magnitude);
			}

			MoveCamera(relativeTargetPosition, targetSize, LevelManager.cameraFocusTarget);
		}

		private void MoveCamera (Vector3 relativeTargetPosition, float targetSize, Transform targetParent = null)
		{
			//set the appropiate parent. Default to player character
			if (targetParent == null) { targetParent = LevelManager.playerGameObject.transform; }
			if (transform.parent != targetParent) {
				transform.SetParent(targetParent);
			}

			//predict player character future position and move the camera ahead of the player
			transform.localPosition = Vector3.Lerp(transform.localPosition, relativeTargetPosition, lerpRate);
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