using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    [SerializeField]
    private AudioClip coinClip, lifeClip;

    private CameraMovement cameraScript;
    private Vector3 lastPosition;
    private bool countScore;

    public static int scoreCount;
    public static int lifeCount;
    public static int coinScore;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraMovement>();
    }

    // Use this for initialization
    void Start () {
        lastPosition = transform.position;
        countScore = true;
	}
	
	// Update is called once per frame
	void Update () {
        CountScore();

    }
    
    void CountScore()
    {
        if(countScore)
        {
            if(transform.position.y < lastPosition.y)
            {
                scoreCount++;
            }
            lastPosition = transform.position;
            GameplayController.instance.PlayerScore(scoreCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            coinScore++;
            scoreCount += 10;
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);

            GameplayController.instance.CoinScore(coinScore);
            GameplayController.instance.PlayerScore(scoreCount);
        }

        if(target.tag == "Life")
        {
            lifeCount++;
            scoreCount += 50;
            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            target.gameObject.SetActive(false);

            GameplayController.instance.LifeScore(lifeCount);
            GameplayController.instance.PlayerScore(scoreCount);
        }

        if(target.tag == "Deadly")
        {
            countScore = false;
            cameraScript.moveCamera = false;

            transform.position = new Vector2(500, 500);
            lifeCount--;

            GameplayController.instance.GameoverScore(scoreCount, coinScore);
        }

        if(target.tag == "Bounds")
        {
            countScore = false;
            cameraScript.moveCamera = false;

            transform.position = new Vector2(500, 500);
            lifeCount--;

            GameplayController.instance.GameoverScore(scoreCount, coinScore);
        }
    }
}
