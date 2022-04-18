using UnityEngine;

using static PHATASS.Utils.MathUtils.IntExtensions;
using static PHATASS.Utils.MathUtils.ColorExtensions;

using PHATASS.Utils.RandomUtils;

namespace CatAttack.FX
{
	class SpriteColorHueScroll : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private int direction = 0; //1 = ascending, -1 = descending, 0 = random

		[SerializeField]
		private float speed = 1f; //rate of color change per second

		[SerializeField]
		private SpriteRenderer spriteRenderer;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			//try to find spriteRenderer
			if (this.spriteRenderer == null)
			{ this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>(); }

			//ensure direction is valid and randomize if necessary
			if (this.direction == 0)
			{ this.direction = RandomSign.Int(); }
			this.direction = this.direction.ESign();
		}

		private void Update ()
		{
			//Debug.Log(this.currentHueDelta);
			this.spriteRenderer.color = this.spriteRenderer.color.EChangeHue(this.currentHueDelta);
		}
	//ENDOF MonoBehaviour

	//private properties
		private float currentHueDelta { get { return (Time.deltaTime * this.speed * this.direction); }}
	//ENDOF private properties

	//private methods
	//ENDOF private methods
	}
}