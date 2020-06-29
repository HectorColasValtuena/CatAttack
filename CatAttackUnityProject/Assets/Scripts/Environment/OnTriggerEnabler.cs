using UnityEngine;

namespace CatAttack
{		
	public class OnTriggerEnabler : MonoBehaviour
	{
		public GameObject onOffTarget;
		public bool startDisabled = true;

		public void Awake ()
		{
			if (startDisabled) { onOffTarget.SetActive(false); }
		}

		public void OnTriggerEnter2D (Collider2D other)
		{
			onOffTarget.SetActive(true);
		}

		public void OnTriggerExit2D (Collider2D other)
		{
			onOffTarget.SetActive(false);
		}
	}
}