using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicButtonCommands : MonoBehaviour
{
	public void ExitGame()
	{
		Application.Quit();
	}
	public void StartNextScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
}
