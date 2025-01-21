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
    public TextMeshPro levelText;

    private int selectedLevel;

    void Start()
    {
        print("album count: " + albums.Count);
        print("clips count: " + clips.Count);
        print("levels count: " + levels.Count);
        selectedLevel = 0;

        // Initialize to the first album and audio clip
        if (albums.Count > 0 && clips.Count > 0)
        {
            spriteRenderer.sprite = albums[0];
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clips[0];
            audioSource.Play();
            Debug.Log($"Playing {audioSource.clip.name}");
        }
    }

    void Update() {
        if (albums.Count <= 0 || clips.Count <= 0) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            nextSong();
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            backSong();
        }
    }

    public void nextSong() {
        selectedLevel = (selectedLevel + 1) % albums.Count;
        spriteRenderer.sprite = albums[selectedLevel];

        selectedLevel = (selectedLevel + 1) % clips.Count;
        audioSource.clip = clips[selectedLevel];

        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    public void backSong() {
        selectedLevel = (selectedLevel - 1 + albums.Count) % albums.Count;
        spriteRenderer.sprite = albums[selectedLevel];

        selectedLevel = (selectedLevel - 1 + clips.Count) % clips.Count;
        audioSource.clip = clips[selectedLevel];
        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    public void PlayGame() {
        // string sceneToLoad = "Scenes/Level/" + selectedLevel;
        // Debug.Log("Loading scene: " + sceneToLoad);
        // SceneManager.LoadScene(sceneToLoad);
    }
}
