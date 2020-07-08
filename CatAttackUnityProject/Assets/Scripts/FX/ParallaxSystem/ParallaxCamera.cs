using UnityEngine;

namespace CatAttack
{
	public class ParallaxCamera : MonoBehaviour
	{
		public static Camera main;

		public void Awake ()
		{
			ParallaxCamera.main = GetComponent<Camera>();
			Debug.Log(ParallaxCamera.main);
		}
	}
}