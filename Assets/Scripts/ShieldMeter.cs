using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShieldMeter : MonoBehaviour
{
    private float maxMeterValue = 50;
    private float currentMeterValue;
    [SerializeField] private Image meterFill;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Gradient colorGradient;

    /// <summary>
    /// Initializes the shield meter at 0.
    /// </summary>
    void Start()
    {
        currentMeterValue = 0;
        updateMeterValueBar();
    }

    /// <summary>
    /// Returns the current value of the shield meter.
    /// </summary>
    public float getCurrentMeterValue() {
        return currentMeterValue;
    }

    /// <summary>
    /// Returns the maximum possible value of the shield meter.
    /// </summary>
    public float getMaxMeterValue() {
        return maxMeterValue;
    }

    /// <summary>
    /// Sets the shield meter to a specific value and updates the UI.
    /// </summary>
    /// <param name="value">The new value to set.</param>
    public void setMeterValue(float value) {
        currentMeterValue = value;
        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);
        updateMeterValueBar();
    }

    /// <summary>
    /// Adjusts the shield meter by a given amount and updates the UI.
    /// </summary>
    /// <param name="amount">The amount to change the meter by (positive or negative).</param>
    public void updateMeterValue(float amount) {
        currentMeterValue += amount;
        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);
        updateMeterValueBar();
    }

    /// <summary>
    /// Updates the visual representation of the meter (fill amount and color) based on the current value.
    /// </summary>
    private void updateMeterValueBar() {
        float targetFillAmount = currentMeterValue / maxMeterValue;
        meterFill.DOFillAmount(targetFillAmount, fillSpeed);
        meterFill.DOColor(colorGradient.Evaluate(targetFillAmount), fillSpeed);
    }
}
