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
    public HealthBar healthBar;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro comboText;
    public TMPro.TextMeshPro hpText;
    static int combo;
    static int score;
    static float health;
    void Start()
    {
        Instance = this;
        combo = 0;
        score = 0;
        health = Instance.healthBar.getMaxHealth();
    }
    public static void Hit()
    {
        combo += 1;
        score += 1;
        if (Instance.healthBar.getMaxHealth() - Instance.healthBar.getCurrentHealth() >= 1) {
            health += 1;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.hitSFX.Play();
    }
    public static void InaccurateHit() {
        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 1;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.inaccuratehitSFX.Play();

        if (health == 0) {
            print("GAME OVER LOL");
        }
    }
    public static void Miss()
    {
        combo = 0;
        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 4;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.missSFX.Play();

        if (health == 0) {
            print("GAME OVER LOL");
        }
    }
    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        comboText.text = "Combo: " + combo.ToString();
        hpText.text = "HP: " + health.ToString("F0");
    }
}
