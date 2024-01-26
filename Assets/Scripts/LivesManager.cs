using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] GameObject[] livesObjects;
    [SerializeField] GameObject gameOverPanel;

    int lives = 3;
    bool isGameOver = false;

    private void Update()
    {
        if (lives <= 0 && !isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            GetComponent<ScoreManager>().SavePlayerScore();
        }
    }

    public void LoseLife()
    {
        lives--;
        livesObjects[lives].GetComponent<Image>().color = Color.black;
    }

    public void IncreaseLives()
    {
        if (lives >= 3) return;
        livesObjects[lives].GetComponent<Image>().color = Color.white;
        lives++;
    }

}
