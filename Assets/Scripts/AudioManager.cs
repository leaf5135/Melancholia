using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("RhythmGame").isLoaded) {
            Destroy(gameObject);
        }
    }
}
