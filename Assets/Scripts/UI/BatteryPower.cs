using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour
{
    public static BatteryPower instance;
    [SerializeField] private Image batteryFgImage;

    [SerializeField] private float drainTime = 15.0f;

    [SerializeField] private float remainingPower;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        remainingPower = batteryFgImage.fillAmount;
    }

    void Update()
    {
        if (GameManager.FlashLightActive || GameManager.NightVisionActive)
        {
            batteryFgImage.fillAmount -= 1.0f / drainTime * Time.deltaTime;
            remainingPower = batteryFgImage.fillAmount;
            GameManager.BatteryPower = remainingPower;
        }
    }

    public void ReplenishBattery(float replenishAmount)
    {
        batteryFgImage.fillAmount += replenishAmount / 100;
        remainingPower = batteryFgImage.fillAmount;
        GameManager.BatteryPower = remainingPower;
        GameManager.Batteries--;
    }
}
