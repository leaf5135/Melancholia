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

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
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

    public void ResumeGame()
    {
        isPaused = false;
        userInterface.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
        userInterface.SetActive(false);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnHome()
    {
        ResumeGame();
        SceneManager.LoadScene("Home");
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            print("LEVEL COMPLETE");
            gameResults.text = "LEVEL COMPLETE\n\n" +
                                "<size=50>Score: " + StatsManager.score.ToString() + "</size>\n" +
                                "<size=50>Combo: " + StatsManager.combo.ToString() + "</size>";

        }
        else
        {
            print("GAME OVER");
            gameResults.text = "GAME OVER\n\n" +
                                "<size=50>Score: " + StatsManager.score.ToString() + "</size>\n" +
                                "<size=50>Combo: " + StatsManager.combo.ToString() + "</size>";

        }

        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine()
    {
        isPaused = true;
        yield return new WaitForSeconds(0.5f);
        userInterface.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
