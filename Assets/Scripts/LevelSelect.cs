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

    void Start()
    {
        print("levels count: " + levels.Count);
        selectedLevel = 0;

        // Initialize to the first album and audio clip
        if (albums.Count > 0 && clips.Count > 0 && levels.Count > 0)
        {
            spriteRenderer.sprite = albums[0];
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clips[0];
            audioSource.Play();
            Debug.Log($"Playing {audioSource.clip.name}");
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Home");
        }

        if (albums.Count <= 0 || clips.Count <= 0 || levels.Count <= 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            nextSong();
        }
        else if (Input.GetKeyDown(KeyCode.Z)) {
            backSong();
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            PlayGame();
        }

        levelText.text = levels[selectedLevel];
    }

    public void nextSong() {
        selectedLevel = (selectedLevel + 1) % levels.Count;
        spriteRenderer.sprite = albums[selectedLevel];
        audioSource.clip = clips[selectedLevel];

        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    public void backSong() {
        selectedLevel = (selectedLevel - 1 + levels.Count) % levels.Count;
        spriteRenderer.sprite = albums[selectedLevel];
        audioSource.clip = clips[selectedLevel];

        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    public void PlayGame() {
        string sceneToLoad = "Scenes/Level/" + selectedLevel;
        Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
