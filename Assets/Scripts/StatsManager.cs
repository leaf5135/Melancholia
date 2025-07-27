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
    public ShieldMeter shieldMeter;
    public TextMeshPro comboText;
    public TextMeshPro scoreText;
    public TextMeshPro hpText;
    public TextMeshPro meterText;

    public static int maxCombo;
    public static int combo;
    public static int score;
    public static float health;
    public static bool gameOver;
    public static float meter;
    public static bool isMeterFull;

    private bool shieldActive;
    private int shieldTimeRemaining;
    private bool flash;

    /// <summary>
    /// Initializes game stats and activates the shield at game start.
    /// </summary>
    void Start()
    {
        Instance = this;
        maxCombo = 0;
        combo = 0;
        score = 0;
        meter = 0;
        health = Instance.healthBar.getMaxHealth();
        gameOver = false;
        isMeterFull = false;
        shieldTimeRemaining = 0;
        flash = false;
        ActivateShield(3);
    }

    /// <summary>
    /// Triggers the shield coroutine with a specified duration.
    /// </summary>
    /// <param name="duration">Shield active time in seconds.</param>
    public void ActivateShield(int duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    /// <summary>
    /// Coroutine for managing shield duration and visual flashing.
    /// </summary>
    /// <param name="duration">Shield active time in seconds.</param>
    private IEnumerator ShieldCoroutine(int duration)
    {
        shieldActive = true;
        print("shield activated");
        int counter = duration * 2;
        while (counter > 0)
        {
            shieldTimeRemaining = counter / 2;
            counter -= 1;
            flash = !flash;
            yield return new WaitForSeconds(0.5f);
        }

        shieldActive = false;
        print("shield no longer active");
        scoreText.color = Color.white;
        comboText.color = Color.white;
        hpText.color = Color.white;
        meterText.color = Color.white;
    }

    /// <summary>
    /// Called when a note is hit. Increases score, combo, and meter. Recovers health.
    /// </summary>
    public static void Hit()
    {
        if (gameOver) return;

        combo += 1;
        score += 1;
        if (combo > maxCombo) {
            maxCombo = combo;
        }
        if (Instance.healthBar.getMaxHealth() - Instance.healthBar.getCurrentHealth() >= 1) {
            health += 1;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.hitSFX.Play();

        if (Instance.shieldMeter.getCurrentMeterValue() < Instance.shieldMeter.getMaxMeterValue()) {
            meter += 1;
            meter = Mathf.Clamp(meter, 0, Instance.shieldMeter.getMaxMeterValue());
            Instance.shieldMeter.setMeterValue(meter);

            if (meter >= 50) {
                isMeterFull = true;
            }
        }
    }

    /// <summary>
    /// Called when an inaccurate note is hit. Reduces health unless shield is active.
    /// </summary>
    public static void InaccurateHit()
    {
        if (gameOver || Instance.shieldActive) return;

        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 1;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.inaccuratehitSFX.Play();

        if (health <= 0) {
            gameOver = true;
        }
    }

    /// <summary>
    /// Called when a note is missed. Heavily reduces health and resets combo.
    /// </summary>
    public static void Miss()
    {
        if (gameOver || Instance.shieldActive) return;

        combo = 0;
        if (Instance.healthBar.getCurrentHealth() > 0) {
            health -= 3;
            health = Mathf.Clamp(health, 0, Instance.healthBar.getMaxHealth());
            Instance.healthBar.setHealth(health);
        }
        Instance.missSFX.Play();

        if (health <= 0) {
            gameOver = true;
        }
    }

    /// <summary>
    /// Updates UI elements every frame, handles shield activation via key input.
    /// </summary>
    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        comboText.text = "Combo: " + combo.ToString();
        hpText.text = "HP: " + health.ToString("F0");
        if (isMeterFull) {
            meterText.text = "ACTIVATE (P)";
        } else {
            meterText.text = "Shield Meter";
        }

        if (Input.GetKeyDown(KeyCode.P) && isMeterFull) {
            ActivateShield(3);
            meter = 0;
            meter = Mathf.Clamp(meter, 0, Instance.shieldMeter.getMaxMeterValue());
            Instance.shieldMeter.setMeterValue(meter);
            isMeterFull = false;
        }

        if (shieldActive)
        {
            Color flashDim = new Color(200 / 255f, 150 / 255f, 250 / 255f, 0.5f);
            Color flashLit = new Color(200 / 255f, 150 / 255f, 250 / 255f, 1.0f);
            scoreText.color = flash ? flashDim : flashLit;
            comboText.color = flash ? flashDim : flashLit;
            hpText.color = flash ? flashDim : flashLit;
            meterText.color = flash ? flashDim : flashLit;
            meterText.text = "SHIELD: " + shieldTimeRemaining + "s";
        }
    }
}
