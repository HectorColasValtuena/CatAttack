using SerializeFieldAttribute = UnityEngine.SerializeField;

using Debug = UnityEngine.Debug;
using Animator = UnityEngine.Animator;
using TouchPhase = UnityEngine.TouchPhase;
using RectTransform = UnityEngine.RectTransform;
using Vector2 = UnityEngine.Vector2;

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
		bool IButton.pressed { get { return this.IsPressed();}}
	//ENDOF IButton

	//MonoBehaviour lifecycle
		private void Awake ()
		{
			this.rectTransform = (this.transform as RectTransform);
			Debug.Log(this.rectTransform.rect);
		}

		private void Update ()
		{
			this.SetAnimatorState(this.IsPressed());
		}
	//ENDOF MonoBehaviour lifecycle

	//private fields
		RectTransform rectTransform;
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
			return this.rectTransform.rect.Contains(position);
		}
	//ENDOF private methods
	}
}