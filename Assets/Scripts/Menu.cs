using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenAbout()
    {
        string url = "https://andrewdiep1.github.io/gdc-web/";
        Application.OpenURL(url);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
