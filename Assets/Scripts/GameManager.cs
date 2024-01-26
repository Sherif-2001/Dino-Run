using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int maxScore;
    [SerializeField] TextMeshProUGUI maxScoreText;

    float currentTimeScale;

    private void Start()
    {
        Time.timeScale = 1;
        if (maxScoreText != null)
        {
            maxScore = PlayerPrefs.GetInt("MaxScore", 0);
            maxScoreText.text = "Max Score\n\n" + maxScore;
        }
    }

    private void Update()
    {
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft && Screen.orientation != ScreenOrientation.LandscapeLeft))
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight && Screen.orientation != ScreenOrientation.LandscapeRight))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }

    /// <summary>
    /// Pause the game by stopping time
    /// </summary>
    public void PauseGame()
    {
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resume the game by return the flow of time
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = currentTimeScale;
    }

    /// <summary>
    /// Restart the game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Start the game from main menu
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Return to the main menu from the game scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }

}
