using UnityEngine;

using static PHATASS.Utils.Extensions.RectExtensions;

using ScreenUtils = PHATASS.Utils.ScreenUtils;

namespace CatAttack.Input
{
	public class RectTouchButton :
		UnityEngine.MonoBehaviour,
		IButton
	{
	//serialized fields
		[SerializeField]
		private Animator backgroundAnimator;

		[SerializeField]
		private Animator iconAnimator;
	//ENDOF serialized

	//IButton implementation
		bool IButton.pressed { get { return this.pressed;}}
		private bool pressed = false;
	//ENDOF IButton

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			this.rectTransform = (this.transform as RectTransform);
			this.anchorRect = this.RectFromTransformAnchors(this.rectTransform);

			Debug.Log(this.name + " " + this.anchorRect);
		}

		private void Update ()
		{
			this.pressed = this.IsPressed();
			this.SetAnimatorState(this.IsPressed());
		}
	//ENDOF MonoBehaviour lifecycle

	//private fields
		private RectTransform rectTransform;
		private Rect anchorRect;
	//ENDOF private fields

	//private methods
		//returns true if any touch is within this rect transform's bounds
		private bool IsPressed ()
		{
		
			foreach (UnityEngine.Touch touch in UnityEngine.Input.touches)
			{
				if (
					touch.phase == TouchPhase.Began
				 || touch.phase == TouchPhase.Moved
				 || touch.phase == TouchPhase.Stationary
				) {
					Debug.Log("Touch@: " + touch.position);
					if (this.PositionIsWithinRect(touch.position))
					{ return true; }
				}
			}
			return false;
		}

		//sets animators pressed state 
		private void SetAnimatorState (bool pressedState)
		{
			backgroundAnimator?.SetBool("Pressed", pressedState);
			iconAnimator?.SetBool("Pressed", pressedState);
		}

		//determines if a touched position is within the rect of this button
		private bool PositionIsWithinRect (Vector2 position)
		{
			Vector2 normalizedPosition = ScreenUtils.PixelToNormalizedScreenPosition(position);
			return this.anchorRect.Contains(normalizedPosition);
		}

		//creates a rect delimiting the anchors of this RectTransform
		private Rect RectFromTransformAnchors (RectTransform rectTransform)
		{
			Vector2[] anchors = {rectTransform.anchorMin, rectTransform.anchorMax};
			return anchors.ERectFromPoints();
		}
	//ENDOF private methods
	}
}