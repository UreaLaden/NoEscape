using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupButton : MonoBehaviour
{
    [SerializeField] private int replenishAmount = 25;
    public bool itemConsumed = false;
    [SerializeField] private Item.ItemType _itemType;
    void Start()
    {
        GameManager.OnConsumeItem?.AddListener(ConsumeItem);
    }

    private void ConsumeItem()
    {
        if (itemConsumed)
        {
            gameObject.SetActive(false);
            SetReplenishAmount(_itemType);
            GameManager.ItemConsumed = false;
            GameManager.OnConsumeItem.RemoveListener(ConsumeItem);
        }
    }

    public void ConsumeApple()
    {        
        itemConsumed = GameManager.PlayerHealth < 100;
        GameManager.ItemConsumed = GameManager.PlayerHealth < 100;
        AudioManager.Instance.pickupSounds[0].Play();
    }

    public void ReplenishBattery()
    {
        itemConsumed = GameManager.BatteryPower < 1;
        GameManager.ItemConsumed = GameManager.BatteryPower < 1;
        AudioManager.Instance.pickupSounds[1].Play();
    }

    private void SetReplenishAmount(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.APPLE:
                GameManager.amountToRestore = GameManager.PlayerHealth >= 100 ? 0 : replenishAmount;
                break;
            case Item.ItemType.BATTERY:
                BatteryPower.instance.ReplenishBattery(replenishAmount);
                GameManager.amountToRestore = 0;
                break;
        }
    }
   
}
