using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject userInterface;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public static bool isPaused;
    public KeyCode pauseKey;

    // Start is called before the first frame update
    void Start()
    {
        userInterface.SetActive(true);
        isPaused = false;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsManager.gameOver)
        {
            GameOver();
        }
        else if (Input.GetKeyDown(pauseKey))
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

    public void PauseGame()
    {
        userInterface.SetActive(false);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        userInterface.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnHome()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Home");
    }

    public void GameOver()
    {
        userInterface.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
