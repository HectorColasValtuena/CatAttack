using UnityEngine;

public class AnimatorBoolSetter : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	public void Set (string parameterName, bool value)
	{
		this.animator.SetBool(parameterName, value);
	}

	public void SetTrue (string parameterName)
	{ this.Set(parameterName, true); }

	public void SetFalse (string parameterName)
	{ this.Set(parameterName, false); }
}