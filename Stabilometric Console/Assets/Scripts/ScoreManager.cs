using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text scoreText;
    public Text hiScoreText;

    public float score;
    public float hiScore;
    public float pointsPerSec;

    void Start(){

    }

    void Update(){
        score += pointsPerSec * Time.deltaTime;

        if (score > PlayerPrefs.GetFloat("HighScore", 0)){
            PlayerPrefs.SetFloat("HighScore", score);
        }

        scoreText.text = "Score: " + Mathf.Round(score);
        hiScoreText.text = "High Score: " + Mathf.Round(PlayerPrefs.GetFloat("HighScore", score));
    }
}