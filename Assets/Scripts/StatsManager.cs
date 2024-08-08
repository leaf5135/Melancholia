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
    static int combo;
    static int score;
    void Start()
    {
        Instance = this;
        combo = 0;
        score = 0;
    }
    public static void Hit()
    {
        combo += 1;
        score += 1;
        if (Instance.healthBar.getMaxHealth() - Instance.healthBar.getCurrentHealth() >= 1) {
            Instance.healthBar.updateHealth(1);
        }
        Instance.hitSFX.Play();
    }
    public static void InaccurateHit() {
        if (Instance.healthBar.getCurrentHealth() > 0) {
            Instance.healthBar.updateHealth(-1);
        }
        Instance.inaccuratehitSFX.Play();
    }
    public static void Miss()
    {
        combo = 0;
        if (Instance.healthBar.getCurrentHealth() > 0) {
            Instance.healthBar.updateHealth(-4);
        }
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = "Score - " + score.ToString();
        comboText.text = "Combo - " + combo.ToString();
    }
}
