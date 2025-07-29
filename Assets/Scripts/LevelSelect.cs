using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public List<Sprite> albums = new List<Sprite>();
    public List<AudioClip> clips = new List<AudioClip>();
    public List<string> levels = new List<string>();
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public TextMeshProUGUI levelText;

    private int selectedLevel;

    /// <summary>
    /// Initializes the level select screen, sets the first album, audio clip, and plays the audio.
    /// </summary>
    void Start()
    {
        selectedLevel = 0;

        // Initialize to the first album and audio clip
        if (albums.Count > 0 && clips.Count > 0 && levels.Count > 0)
        {
            spriteRenderer.sprite = albums[0];
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clips[0];
            audioSource.Play();
            // Debug.Log($"Playing {audioSource.clip.name}");
        }
    }

    /// <summary>
    /// Checks for user input every frame to navigate between songs, play selected level, or go back to home.
    /// Updates the displayed level text.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }

        if (albums.Count <= 0 || clips.Count <= 0 || levels.Count <= 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            nextSong();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            backSong();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }

        levelText.text = levels[selectedLevel];
    }

    /// <summary>
    /// Advances to the next song/level, updates the displayed album and plays the corresponding audio clip.
    /// </summary>
    public void nextSong()
    {
        selectedLevel = (selectedLevel + 1) % levels.Count;
        spriteRenderer.sprite = albums[selectedLevel];
        audioSource.clip = clips[selectedLevel];

        // Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    /// <summary>
    /// Moves back to the previous song/level, updates the displayed album and plays the corresponding audio clip.
    /// </summary>
    public void backSong()
    {
        selectedLevel = (selectedLevel - 1 + levels.Count) % levels.Count;
        spriteRenderer.sprite = albums[selectedLevel];
        audioSource.clip = clips[selectedLevel];

        // Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    /// <summary>
    /// Loads the currently selected level's scene.
    /// </summary>
    public void PlayGame()
    {
        string sceneToLoad = "Scenes/Level/" + selectedLevel;
        // Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
