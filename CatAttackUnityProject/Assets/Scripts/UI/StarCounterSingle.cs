using UnityEngine;

namespace CatAttack.UI
{
	public class StarCounterSingle : MonoBehaviour
	{
		public GameObject starFill;

		public bool full
		{
			get
			{
				return starFill.activeSelf;
			}
			set 
			{
				starFill.SetActive(value);
			}
		}

		public bool active
		{
			get
			{
				return gameObject.activeSelf;
			}
			set
			{
				gameObject.SetActive(value);
			}
		}

	}
}