using UnityEngine;

namespace CatAttack
{
	public class ParallaxSettings : MonoBehaviour
	{
		//static
		public static ParallaxSettings instance;
		//instance
		public Transform anchor;
		public float anchorMaxY = 80;
		public float anchorMinY = -20;
		public float maxYRange;

		void Awake ()
		{
			ParallaxSettings.instance = this;
			if (anchor == null) { anchor = Camera.main.transform;}//LevelManager.playerGameObject.transform; }//ParallaxCamera.main.transform; }
			Debug.Log("ParallaxSettings anchor");
			Debug.Log(anchor);
			maxYRange = anchorMaxY - anchorMinY;
		}
	}
}