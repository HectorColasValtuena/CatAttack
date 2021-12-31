using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGame : MonoBehaviour
{
	public void StartGame ()
	{
		SceneManager.LoadScene(1); //TO-DO move this to a constant somewhere safe
	}
}
