using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public SpriteRenderer sr;
    public List<Sprite> albums = new List<Sprite>();
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource audioSource;
    private int selectedScene = 0;
    private int selectedSong = 0;
    public GameObject songAlbum;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            nextSong();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            backSong();
        }
    }

    public void nextSong() {
        print("album count: " + albums.Count);
        print("clips count: " + clips.Count);

        selectedSong = (selectedSong + 1) % albums.Count;
        sr.sprite = albums[selectedSong];

        selectedScene = (selectedScene + 1) % clips.Count;
        audioSource.clip = clips[selectedScene];

        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    public void backSong() {
        print("album count: " + albums.Count);
        print("clips count: " + clips.Count);

        selectedSong = (selectedSong - 1 + albums.Count) % albums.Count;
        sr.sprite = albums[selectedSong];

        selectedScene = (selectedScene - 1 + clips.Count) % clips.Count;
        audioSource.clip = clips[selectedScene];
        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }

    // Start is called before the first frame update
    public void PlayGame(){
        Debug.Log(sr.sprite.name);
        SceneManager.LoadScene(sr.sprite.name);
    }
}
