using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public AudioSource hitSFX;
    public AudioSource inaccuratehitSFX;
    public AudioSource missSFX;
    public HealthBar healthBar;
    public TextMeshPro scoreText;
    public TextMeshPro comboText;
    public TextMeshPro hpText;

    private static int combo;
    private static int score;
    private static float health;
    private bool isInGracePeriod = true;
    private int elapsed = 0;
    private Color flashColor = new Color(255, 255, 255, 0.5f);
    private bool flash = false;

    void Start()
    {
        Instance = this;
        combo = 0;
        score = 0;
        health = Instance.healthBar.getMaxHealth();
        StartCoroutine(GracePeriodCoroutine());
    }

    private IEnumerator GracePeriodCoroutine()
    {
        while (elapsed < 10)
        {
            elapsed += 1;
            flash = !flash;
            yield return new WaitForSeconds(0.5f);
        }

        isInGracePeriod = false;
        scoreText.color = Color.white;
        comboText.color = Color.white;
        hpText.color = Color.white;
        print("GRACE PERIOD OVER");
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
        if (Instance.isInGracePeriod) return;

        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 1;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.inaccuratehitSFX.Play();

        // if (health == 0) {
        //     print("GAME OVER");
        // }
    }
    public static void Miss()
    {
        if (Instance.isInGracePeriod) return;

        combo = 0;
        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 3;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.missSFX.Play();

        // if (health == 0) {
        //     print("GAME OVER");
        // }
    }
    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        comboText.text = "Combo: " + combo.ToString();
        hpText.text = "HP: " + health.ToString("F0");

        if (isInGracePeriod)
        {
            scoreText.color = flash ? flashColor : Color.white;
            comboText.color = flash ? flashColor : Color.white;
            hpText.color = flash ? flashColor : Color.white;
            hpText.text = "Grace: " + (5 - elapsed/2) + "s";
        }
    }
}
