using static PHATASS.Utils.Extensions.Vector2Extensions;

using UnityEngine;

using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;
using IFloatRange = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<System.Single>;

namespace CatAttack.Miscellaneous
{
	public class PhysicsKickOnStart : MonoBehaviour
	{
	//Serialized properties
		[Tooltip("Random range of the direction of the force (Euler angles on Z axis)")]
		[SerializeField]
		private RandomFloatRange _directionRange;
		private IFloatRange directionRange { get { return this._directionRange; }}


		[Tooltip("Random range of force intensity")]
		[SerializeField]
		private RandomFloatRange _forceRange;
		private IFloatRange forceRange { get { return this._forceRange; }}

		[Tooltip("Random range of rotational force")]
		[SerializeField]
		private RandomFloatRange _torqueRange;
		private IFloatRange torqueRange { get { return this._torqueRange; }}

		[Tooltip("Target rigidbody to which effect will be applied - if none will be self's rigidbody")]
		[SerializeField]
		private Rigidbody2D targetRigidbody;
	//ENDOF Serialized properties

	//MonoBehaviour
		private void Start ()
		{
			if (this.targetRigidbody == null) { this.targetRigidbody = this.GetComponent<Rigidbody2D>(); }
			this.Kick();
		}
	//ENDOF MonoBehaviour

	//Private properties
		private Vector2 randomForceVector
		{ get { return this.directionRange.random.EDegreesToVector2() * this.forceRange.random; }}
	//ENDOF Private properties

	//Private methods
		private void Kick ()
		{
			this.targetRigidbody.AddForce(this.randomForceVector, ForceMode2D.Impulse);
			this.targetRigidbody.AddTorque(this.torqueRange.random, ForceMode2D.Impulse);
		}
	//ENDOF Private methods
	}
}