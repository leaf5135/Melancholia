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
    private int selectedSource = 0;

    private int selectedSong = 0;
    public GameObject songAlbum;


    public void nextSong() {
        selectedSong = selectedSong + 1;
        if (selectedSong == albums.Count) {
            selectedSong = 0;
        }
        sr.sprite = albums[selectedSong];

        selectedSource = selectedSource + 1;
        if (selectedSource == clips.Count) {
            selectedSource = 0;
        }
        audioSource.clip = clips[selectedSource];
        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }
    

    public void BackOption() {
        selectedSong = selectedSong - 1;
        if (selectedSong < 0) {
            selectedSong = albums.Count - 1;
        }
        sr.sprite = albums[selectedSong];

        selectedSource = selectedSource - 1;
        if (selectedSource == clips.Count) {
            selectedSource = 0;
        }
        audioSource.clip = clips[selectedSource];
        Debug.Log($"Playing {audioSource.clip.name}");
        audioSource.Play();
    }
    // Start is called before the first frame update
    public void PlayGame(){
        Debug.Log(sr.sprite.name);
        
        SceneManager.LoadScene(sr.sprite.name);
    }
}
