using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static int PlayerHealth = 90;
    public static float BatteryPower;
    public static UnityEvent OnHealthChanged;
    public static bool PlayerHealthChanged = false;
    public static bool FlashLightActive = false;
    public static bool NightVisionActive = false;
    private void Start()
    {
        if (OnHealthChanged == null)
        {
            OnHealthChanged = new UnityEvent();
        }
    }

    private void Update()
    {
        if (PlayerHealthChanged)
        {
            OnHealthChanged?.Invoke();
        }
    }
}
