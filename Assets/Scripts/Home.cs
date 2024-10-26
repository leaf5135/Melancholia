using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SongSelect");
    }

    public void OpenAbout()
    {
        string url = "https://andrewdiep1.github.io/gdc-web/";
        Application.OpenURL(url);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // If in editor, stop playing instead of quitting
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If not in editor, quit the application
        Application.Quit();
        #endif
    }
}
