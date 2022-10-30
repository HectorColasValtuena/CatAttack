using UnityEngine;
using static PHATASS.Utils.Extensions.FloatExtensions;

namespace CatAttack
{
	public class PositionClampY : MonoBehaviour
	{
		[SerializeField]
		private float minimumY = 0f;
		[SerializeField]
		private float maximumY = 10f;

	//MonoBehaviour lifecycle
		private void LateUpdate ()
		{
			this.transform.position = new Vector3 (
				x: this.transform.position.x,
				y: this.transform.position.y.EClamp(this.minimumY, this.maximumY),
				z: this.transform.position.z
			);
		}
	//ENDOF MonoBehaviour
	}
}