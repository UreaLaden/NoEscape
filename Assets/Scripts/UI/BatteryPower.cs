using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour
{
    [SerializeField] private Image batteryFgImage;

    [SerializeField] private float drainTime = 15.0f;

    [SerializeField] private float remainingPower;

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
}
