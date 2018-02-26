using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : SingletonBehaviour<ScoreManager>
{
    [SerializeField] Text HighScore;
    [SerializeField] Text Score;

    private void Start()
    {
        LoadScore();
    }

    public void SaveScore(int score)
    {
        int hs = PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.SetInt("Score", score);
        if (hs < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void LoadScore()
    {
        int hs = PlayerPrefs.GetInt("HighScore");
        int score = PlayerPrefs.GetInt("Score");

        HighScore.text = hs.ToString();
        Score.text = score.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        SceneManager.LoadScene(2);
    }
}
