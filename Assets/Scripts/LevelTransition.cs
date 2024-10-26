using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public Animator animator;

    public string levelToLoad;
 

    // Update is called once per frame
    void Update()
    {
   
    }

    public void FadeToLevel (string level) {
        levelToLoad = level;
        animator.SetTrigger("Fade_Out");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }
}
