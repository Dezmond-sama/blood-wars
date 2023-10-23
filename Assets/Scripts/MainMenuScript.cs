using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
	public void RunGame(string sceneName)
    {
		PlayerPrefs.SetInt ("CurrentScore", 0);
		SceneManager.LoadScene (sceneName);
    }

	public void MainMenu(string sceneName)
	{
		PlayerPrefs.SetInt ("CurrentScore", 0);
		SceneManager.LoadScene (sceneName);
	}

	public void CloseGame()
	{
		Application.Quit ();
	}
}
