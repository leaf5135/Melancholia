using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject userInterface;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI gameResults;
    public static bool isPaused;

    /// <summary>
    /// Starts with unpaused gameplay.
    /// </summary>
    void Start()
    {
        ResumeGame();
    }

    /// <summary>
    /// Handles pause/resume input and triggers end game when conditions are met.
    /// </summary>
    void Update()
    {
        if (StatsManager.gameOver)
        {
            EndGame(false);
        }
        else if (SongManager.songFinished)
        {
            EndGame(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    /// <summary>
    /// Resumes gameplay by enabling UI and unpausing time.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        userInterface.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Pauses gameplay by disabling UI and stopping time.
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        userInterface.SetActive(false);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Restarts the current scene and resumes gameplay.
    /// </summary>
    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Returns the player to the home screen and resumes gameplay.
    /// </summary>
    public void ReturnHome()
    {
        ResumeGame();
        SceneManager.LoadScene("Home");
    }

    /// <summary>
    /// Displays game over or win results and starts the end game routine.
    /// </summary>
    /// <param name="win">Whether the level was completed successfully.</param>
    public void EndGame(bool win)
    {
        if (win)
        {
            gameResults.text = "LEVEL COMPLETE\n\n" +
                                "<size=50>Max Combo: " + StatsManager.maxCombo.ToString() + "</size>\n" +
                                "<size=50>Score: " + StatsManager.score.ToString() + "</size>";
        }
        else
        {
            gameResults.text = "GAME OVER\n\n" +
                                "<size=50>Max Combo: " + StatsManager.maxCombo.ToString() + "</size>\n" +
                                "<size=50>Score: " + StatsManager.score.ToString() + "</size>";
        }

        StartCoroutine(EndGameRoutine());
    }

    /// <summary>
    /// Coroutine that displays the game over menu after a brief delay and pauses the game.
    /// </summary>
    private IEnumerator EndGameRoutine()
    {
        isPaused = true;
        yield return new WaitForSeconds(0.33f);
        userInterface.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
