using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    public static UnityEvent OnHealthChanged;
    public static UnityEvent OnItemInView;
    
    public static Item.ItemType currentItemInView;
    
    public static int PlayerHealth = 90;
    public static float BatteryPower;
    public static bool PlayerHealthChanged = false;
    public static bool ItemInView = false;
    public static bool FlashLightActive = false;
    public static bool NightVisionActive = false;
    public static bool isPaused = false;
    public static int Apples = 0;
    public static bool canPickupApple = true;
    private void Start()
    {
        if (OnHealthChanged == null)
        {
            OnHealthChanged = new UnityEvent();
        }

        if (OnItemInView == null)
        {
            OnItemInView = new UnityEvent();
        }
       
    }

    private void Update()
    {
        canPickupApple = Apples < 6;
        if (PlayerHealthChanged)
        {
            OnHealthChanged?.Invoke();
        }

        if (ItemInView)
        {
            OnItemInView?.Invoke();
        }
    }
}
