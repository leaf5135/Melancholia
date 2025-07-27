using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the main menu interactions, including playing theme music,
/// navigating to other scenes, opening URLs, and quitting the game.
/// </summary>
public class Menu : MonoBehaviour
{
    private AudioSource homeTheme;

    /// <summary>
    /// Starts the home screen and plays the home theme music.
    /// </summary>
    public void Start()
    {
        homeTheme = GetComponent<AudioSource>();
        homeTheme.Play();
    }

    /// <summary>
    /// Loads the SongSelect scene to start the game.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SongSelect");
    }

    /// <summary>
    /// Opens the About webpage in the user's default browser.
    /// </summary>
    public void OpenAbout()
    {
        string url = "https://andrewdiep1.github.io/gdc-web/";
        Application.OpenURL(url);
    }

    /// <summary>
    /// Quits the game application.
    /// In the Unity Editor, stops play mode instead of quitting.
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
