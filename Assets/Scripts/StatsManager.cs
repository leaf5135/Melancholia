using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public AudioSource hitSFX;
    public AudioSource inaccuratehitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro comboText;
    static int combo;
    static int score;
    static int health;
    private int maxHealth = 25;
    void Start()
    {
        Instance = this;
        combo = 0;
        score = 0;
        health = maxHealth;
    }
    public static void Hit()
    {
        combo += 1;
        score += 1;
        Instance.hitSFX.Play();
        System.Random rnd = new System.Random();
        int regen  = rnd.Next(1, 25);
        if (regen < 5) {
            health += 1;
        }
    }
    public static void InaccurateHit() {
        combo = 0;
        Instance.inaccuratehitSFX.Play();
        System.Random rnd = new System.Random();
        int dmg  = rnd.Next(1, 8);
        if (dmg <= 4) {
            health -= 1;
        }
    }
    public static void Miss()
    {
        combo = 0;
        health -= 4;
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = "Score - " + score.ToString();
        comboText.text = "Combo - " + combo.ToString();
    }
}
