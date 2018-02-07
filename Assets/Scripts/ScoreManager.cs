using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int coin = 0;
    public static int score = 0;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text coinText;


    [SerializeField]
    private AudioSource coinAudio;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void addScore()
    {
        score+=1;
        scoreText.GetComponent<Text>().text = score.ToString();
        //Debug.Log("addscore");
    }

    public void addCoin()
    {
        coin += 1;
        coinText.GetComponent<Text>().text = coin.ToString();
        coinAudio.Play();
    }
}
