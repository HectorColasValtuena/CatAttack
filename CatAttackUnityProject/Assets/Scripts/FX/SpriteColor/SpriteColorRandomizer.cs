using UnityEngine;

using RandomFloatRange = PHATASS.Utils.MathUtils.Ranges.RandomFloatRange;

using IFloatRange = PHATASS.Utils.MathUtils.Ranges.ILimitedRange<System.Single>;
namespace CatAttack.FX
{
	class SpriteColorRandomizer : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private RandomFloatRange _hueRange;
		private IFloatRange hueRange { get { return this._hueRange; }}

		[SerializeField]
		private RandomFloatRange _saturationRange;
		private IFloatRange saturationRange { get { return this._saturationRange; }}

		[SerializeField]
		private RandomFloatRange _brightnessRange;
		private IFloatRange brightnessRange { get { return this._brightnessRange; }}

		[SerializeField]
		private RandomFloatRange _alphaRange;
		private IFloatRange alphaRange { get { return this._alphaRange; }}
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		private void Start ()
		{
			//try to find sprite
			if (this.spriteRenderer == null)
			{ this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>(); }

			this.spriteRenderer.color = this.GenerateColor();
		}
	//ENDOF MonoBehaviour

	//private methods
		private Color GenerateColor ()
		{
			Color color = Color.HSVToRGB(
				H: this.hueRange.value,
				S: this.saturationRange.value,
				V: this.brightnessRange.value
			);
			
			color.a = this.alphaRange.value;

			return color;
		}
	//ENDOF private methods
	}
}