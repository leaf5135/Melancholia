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

    void Start()
    {
        currentMeterValue = 0;
        updateMeterValueBar();
    }

    public float getCurrentMeterValue() {
        return currentMeterValue;
    }

    public float getMaxMeterValue() {
        return maxMeterValue;
    }

    public void setMeterValue(float value) {
        currentMeterValue = value;
        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);
        updateMeterValueBar();
    }

    public void updateMeterValue(float amount) {
        currentMeterValue += amount;
        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);
        updateMeterValueBar();
    }

    private void updateMeterValueBar() {
        float targetFillAmount = currentMeterValue / maxMeterValue;
        meterFill.DOFillAmount(targetFillAmount, fillSpeed);
        meterFill.DOColor(colorGradient.Evaluate(targetFillAmount), fillSpeed);
    }
}
