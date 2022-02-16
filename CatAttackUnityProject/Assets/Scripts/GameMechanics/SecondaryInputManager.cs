using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAttack
{		
	public class SecondaryInputManager : MonoBehaviour
	{
	//defines
		private string resetKeyCode = "r";

	//In-Class and In-Object management
		public GameObject resetInterfaceElement;

		public void Update()
		{
			CheckResetInput();
		}
	//ENDOF In-Class and In-Object management

	//Player reset input management
		private void CheckResetInput ()
		{
			bool resetInput = UnityEngine.Input.GetKey(resetKeyCode);
			if (resetInterfaceElement.activeSelf != resetInput)
			{
				resetInterfaceElement.SetActive(resetInput);
			}
		}
	//ENDOFPlayer reset input management
	}
}