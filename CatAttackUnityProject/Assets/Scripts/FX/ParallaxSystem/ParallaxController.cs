using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAttack
{
	public class ParallaxController : MonoBehaviour
	{
		public float xParallaxRate;

		public float yMaxPosition;
		public float yMinPosition;
	/*
		public static Transform parallaxTarget;
		public void Awake () {
			if (parallaxTarget != null)
			{
				parallaxTarget = Camera.main.transform;
			}
		}
	*/
		public void Update ()
		{
		//update the position of the parallax system
		//horizontal
			float targetX = 0;

		//vertical
			//float targetY;
		//if the parallax anchor is at its highest position draw the background at its lowest
		//if anchor is at its lowest, draw the background at its lowest
			//Limit	the calculated position of the camera to the limits
			float anchorRelativeY = Mathf.Clamp (
				ParallaxSettings.instance.anchor.position.y,
				ParallaxSettings.instance.anchorMinY,
				ParallaxSettings.instance.anchorMaxY
			);

			//normalize (convert to a 0-1 range) the vertical position of the anchor
			float verticalRatio = (anchorRelativeY - ParallaxSettings.instance.anchorMinY) / ParallaxSettings.instance.maxYRange;

			//convert the normalized value into an Y position.
			//For verticalRatio = 0 => targetY = yMaxPosition. For verticalRatio = 1 => targetY = yMinPosition
			float targetY = yMaxPosition - ((yMaxPosition - yMinPosition) * verticalRatio);

			Debug.Log ("ParallaxController: {\nanchorRelativeY: " + anchorRelativeY + " verticalRatio: " + verticalRatio + " targetY: " + targetY + "\n}");

		//finally move ourselves around the camera anchor
			transform.position = new Vector3 (targetX, targetY, -ParallaxSettings.instance.anchor.position.z) + ParallaxSettings.instance.anchor.position;
		}
	}
}