using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Controls the health bar UI element, updating its fill amount and color based on current health.
/// </summary>
public class HealthBar : MonoBehaviour
{
    private float maxHealth = 25;

    private float currentHealth;

    [SerializeField] private Image healthBarFill;

    [SerializeField] private float fillSpeed;

    [SerializeField] private Gradient colorGradient;

    /// <summary>
    /// Initializes the current health to max health and updates the health bar UI.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    /// <summary>
    /// Gets the current health value.
    /// </summary>
    /// <returns>The current health.</returns>
    public float getCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Gets the maximum health value.
    /// </summary>
    /// <returns>The max health.</returns>
    public float getMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Sets the health to a specific value, clamping it between 0 and max health, then updates the health bar.
    /// </summary>
    /// <param name="health">The new health value.</param>
    public void setHealth(float health)
    {
        currentHealth = health;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
    }

    /// <summary>
    /// Changes the current health by a given amount and updates the health bar.
    /// </summary>
    /// <param name="amount">The amount to change the health by.</param>
    public void updateHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
    }

    /// <summary>
    /// Updates the health bar fill amount and color based on the current health.
    /// </summary>
    private void updateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        healthBarFill.DOColor(colorGradient.Evaluate(targetFillAmount), fillSpeed);
    }
}
