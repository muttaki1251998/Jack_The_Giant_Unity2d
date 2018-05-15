using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void Option()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void Music()
    {

    }
}
