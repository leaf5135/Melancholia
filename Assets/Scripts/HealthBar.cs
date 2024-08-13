using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private float maxHealth = 25;
    private float currentHealth;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradient;

    void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    public float getCurrentHealth() {
        return currentHealth;
    }

    public float getMaxHealth() {
        return maxHealth;
    }

    public void setHealth(float health) {
        currentHealth = health;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
    }

    public void updateHealth(float amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
    }

    private void updateHealthBar() {
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        healthBarFill.DOColor(colorGradient.Evaluate(targetFillAmount), fillSpeed);
    }
}
