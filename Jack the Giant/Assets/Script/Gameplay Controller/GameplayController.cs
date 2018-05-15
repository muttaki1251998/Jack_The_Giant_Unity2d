using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, coinText, lifeText, finalPlayerScore, finalCoinScore;
    [SerializeField]
    private GameObject pausePanel, gameoverPanel;

	// Use this for initialization
	void Awake () {
        MakeInstance();
	}

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    /** Gameover  texts */

    public void GameoverScore(int finalScore, int finalCoinsScored)
    {
        gameoverPanel.SetActive(true);
        finalPlayerScore.text = finalScore.ToString();
        finalCoinScore.text = finalCoinsScored.ToString();
        StartCoroutine(GameoverLoadMainMenu());
    }

    IEnumerator GameoverLoadMainMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("UI");
    }

    /** Gameover  texts */

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("UI");
    }

    public void PlayerScore(int score)
    {
        scoreText.text = "x" + score;
    }

    public void CoinScore(int score)
    {
        coinText.text = "x" + score;
    }

    public void LifeScore(int score)
    {
        lifeText.text = "x" + score;
    }

}
