using UnityEngine;

namespace CatAttack.Extensions
{
	public static class Transform2DExtensions
	{
	//rotates this transform so the +X axis is looking towards the target
		public static void ELookAt2D (this Transform transform, Transform targetTransform)
		{ transform.ELookAt2D(targetTransform.position); }
		public static void ELookAt2D (this Transform transform, Vector3 target)
		{ transform.ELookAt2D((Vector2) target); }
		public static void ELookAt2D (this Transform transform, Vector2 target)
		{
			transform.right = new Vector3 (x: target.x, y: target.y, z: 0f) - transform.position;
		}
	}
}