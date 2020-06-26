using UnityEngine;

namespace CatAttack.UI
{
	public class PredictiveCameraController : MonoBehaviour
	{
		public Vector2 maxAdvanceRate = new Vector2(1.0f, 0.5f);
		public Vector2 maxAdvanceDistance = new Vector2(1.0f, 0.5f);

		public float lerpRate = 0.1f;

		void Update()
		{
			//predict player characters future position and move the camera ahead of the player
			if (Platformer2DUserControl.playerCatRigidbody == null) { Debug.LogWarning("PredictiveCameraController.Update(): No player catracter"); return; }
			Vector3 targetPosition = new Vector3
			(
				CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.x*maxAdvanceRate.x, maxAdvanceDistance.x),
				CapValue(Platformer2DUserControl.playerCatRigidbody.velocity.y*maxAdvanceRate.y, maxAdvanceDistance.y),
				transform.position.z
			);

			//Debug.Log("PredictiveCameraController targetPosition: "); Debug.Log(targetPosition);

			transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, lerpRate);
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