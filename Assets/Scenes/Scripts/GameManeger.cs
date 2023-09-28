using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{

    public AudioSource theMusic;
    
    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManeger instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying) {
            if(Input.anyKeyDown) {
                startPlaying = true;
                theBS.hasStarted = true;
                    theMusic.Play();
                
            }
        }
        
    }
    
    public void NoteHit() {
        Debug.Log("Hit On Time");
    }

    public void NoteMissed() {
        Debug.Log("Note Missed");
    }
}
