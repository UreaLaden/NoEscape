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
    
    public static List<Item.ItemType> availableWeapons = new List<Item.ItemType>(); 
    public static List<Item.ItemType> availableKeys = new List<Item.ItemType>();
    public static GameObject ConsumedItem;
    
    public static int PlayerHealth = 5;
    public static int Apples = 0;
    public static int Batteries = 0;
    public static int Ammo = 0;
    public static int remainingShots = 0;
    public static int Bolts = 0;
    public static int amountToRestore;
    
    public static float BatteryPower;
    public static bool PlayerHealthChanged = false;
    public static bool ItemInView = false;
    public static bool FlashLightActive = false;
    public static bool NightVisionActive = false;
    public static bool isPaused = false;
    public static bool canPickup = true;
    public static bool ItemConsumed = false;

    private void Update()
    {
        if (PlayerHealthChanged){ OnHealthChanged?.Invoke(); }
        if (ItemInView) { OnItemInView?.Invoke(); }
        if (ItemConsumed) { OnConsumeItem?.Invoke(); }
    }
   
}
