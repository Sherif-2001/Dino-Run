using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    float score = 0;


    /// <summary>
    /// Update the score as time passes
    /// </summary>
    void Update()
    {
        score += Time.deltaTime * 60;
        scoreText.text = "Score\n" + Mathf.Round(score);
    }

    /// <summary>
    /// Save player score and max score
    /// </summary>
    public void SavePlayerScore()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore");
        yourScoreText.text = "Score\n" + Mathf.Round(score);

        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", (int)score);
            highScoreText.text = "HighScore\n" + Mathf.Round(score);
        }
        else
        {
            highScoreText.text = "HighScore\n" + Mathf.Round(maxScore);
        }
    }
}
