using UnityEngine;

public class DynamiteQuestController : MonoBehaviour
{
	public GameObject[] dialogList;

	private int phase = 0;

	public void Start()
	{
		
	}

	//Advances the phase index and enables target gameobject within the list. if parameter is negative or omitted will automatically advance 1 step
	public void AdvancePhase (int targetPhase = -1)
	{
		if (targetPhase < 0) { targetPhase = phase+1; }
		if (targetPhase <= phase) { return; }
		phase = targetPhase;
		ChangeDialog(targetPhase);
	}

	private void ChangeDialog (int targetDialog)
	{
		for (int i = 0, iLimit = dialogList.Length; i < iLimit; i++)
		{
			dialogList[i].SetActive(i == targetDialog);
		}
	}
}
