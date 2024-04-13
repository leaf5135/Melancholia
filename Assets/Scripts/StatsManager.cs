using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public static HealthBar bar;
    public AudioSource hitSFX;
    public AudioSource inaccuratehitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro comboText;
    static int combo;
    static int score;
    public int maxHealth = 25;
    public int health;
    void Start()
    {
        Instance = this;
        combo = 0;
        score = 0;
        health = maxHealth;

        // Find the HealthBar component in the scene
        bar = FindObjectOfType<HealthBar>();

        if (bar != null)
        {
            // Set the max health on the HealthBar
            bar.SetMaxHealth(health);
        }
        else
        {
            Debug.LogError("HealthBar not found in the scene!");
        }
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
            bar.SetHealth(health);
        }
    }
    public static void InaccurateHit() {
        combo = 0;
        Instance.inaccuratehitSFX.Play();
        System.Random rnd = new System.Random();
        int dmg  = rnd.Next(1, 8);
        if (dmg <= 4) {
            health -= 1;
            bar.SetHealth(health);
        }
    }
    public static void Miss()
    {
        combo = 0;
        if (score > 0) {
            score -= 1;
        }
        health -= 4;
        bar.SetHealth(health);
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
    }
}
