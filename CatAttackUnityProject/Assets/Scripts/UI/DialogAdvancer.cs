using UnityEngine;

namespace CatAttack
{
	public class DialogAdvancer : MonoBehaviour
	{
		public GameObject[] dialogList;
		public bool resetOnEnable = true;

		public int ActiveDialog
		{
			get { return _activeDialog; }
			set
			{
				for (int i = 0, iLimit = dialogList.Length; i < iLimit; i++)
				{
					_activeDialog = value;
					dialogList[i].SetActive(i == value);
				}
			}
		}

		private int _activeDialog = 0;

		public void OnEnable ()
		{
			if (resetOnEnable) { ActiveDialog = 0;	}
		}

		public void NextDialog ()
		{
			if (ActiveDialog < (dialogList.Length -1)) { ActiveDialog++; }
		}

		public void PreviousDialog ()
		{
			if (ActiveDialog > 0) { ActiveDialog--; }
		}
	}
}