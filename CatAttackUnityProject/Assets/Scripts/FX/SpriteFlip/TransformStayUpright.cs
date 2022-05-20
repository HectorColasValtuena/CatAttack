/*
*	This component inverts the transform's Y scale when Z rotation goes out of the -90/90 degrees range
*	This makes its sprites always look upright, never upside down
*/

using UnityEngine;

namespace CatAttack.FX
{
	public class TransformStayUpright : MonoBehaviour
	{
	//private static fields
		private static Vector3 inversionVector = new Vector3 (1, -1, 1);
	//ENDOF static

	//MonoBehaviour
		private void Start ()
		{
			this.wasInverted = this.isInverted;
		}

		private void LateUpdate ()
		{
			if (this.isInverted != this.wasInverted) { this.Invert(); }
			this.wasInverted = this.isInverted;
		}
	//ENDOF MonoBehaviour

	//private fields
		private bool wasInverted = false;
	//ENDOF private fields

	//private properties
		private bool isInverted 
		{ get {
			float angle = this.zAxisRotation;
			//Debug.Log("angle: " + angle);
			//[TO-DO] optimize this
			if (angle > 270 || angle < -270) { return false; }
			if (angle > 90 || angle < -90) { return true; }
			return false;
		}}

		private float zAxisRotation
		{ get { return this.transform.rotation.eulerAngles.z; }}
	//ENDOF private properties

	//private methods
		private void Invert ()
		{
			this.transform.localScale = Vector3.Scale(this.transform.localScale, inversionVector);
		}
	//ENDOF private methods
	}
}