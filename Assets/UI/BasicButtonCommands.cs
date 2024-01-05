using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicButtonCommands : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
