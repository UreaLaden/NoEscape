using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    public static UnityEvent OnHealthChanged = new UnityEvent();
    public static UnityEvent OnItemInView = new UnityEvent();
    public static UnityEvent OnConsumeItem = new UnityEvent();
    public static Item.ItemType currentItemInView;
    
    public static GameObject ConsumedItem;
    
    public static int PlayerHealth = 5;
    public static int Apples = 0;
    public static int Batteries = 0;
    public static int amountToRestore;
    
    public static float BatteryPower;
    public static bool PlayerHealthChanged = false;
    public static bool ItemInView = false;
    public static bool FlashLightActive = false;
    public static bool NightVisionActive = false;
    public static bool isPaused = false;
    public static bool canPickupApple = true;
    public static bool canPickupBattery = true;
    public static bool canPickup = true;
    public static bool ItemConsumed = false;

    private void Update()
    {
        canPickupApple = Apples < 6;
        canPickupBattery = Batteries < 4;
        
        if (PlayerHealth > 100) { PlayerHealth = 100; }
        if (PlayerHealth < 0) { PlayerHealth = 0; }

        if (PlayerHealthChanged){ OnHealthChanged?.Invoke(); }
        if (ItemInView) { OnItemInView?.Invoke(); }
        if (ItemConsumed) { OnConsumeItem?.Invoke(); }
    }

   
}
