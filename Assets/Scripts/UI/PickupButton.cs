using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupButton : MonoBehaviour
{
    [SerializeField] private int replenishAmount = 25;
    public bool itemConsumed = false;
    void Start()
    {
        GameManager.OnConsumeItem?.AddListener(ConsumeItem);
    }

    private void ConsumeItem()
    {
        if (itemConsumed)
        {
            gameObject.SetActive(false);
            GameManager.amountToRestore = GameManager.PlayerHealth >= 100 ? 0 : replenishAmount;
            GameManager.ItemConsumed = false;
            GameManager.OnConsumeItem.RemoveListener(ConsumeItem);
        }
    }

    public void ConsumeApple()
    {        
        itemConsumed = GameManager.PlayerHealth < 100;
        GameManager.ItemConsumed = GameManager.PlayerHealth < 100;
    }
   
}
