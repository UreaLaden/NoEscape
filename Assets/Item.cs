using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public enum ItemType{APPLE,AMMO}

    public ItemType selectedItem;

    private void Start()
    {
        if (GameManager.OnItemInView == null)
        {
            GameManager.OnItemInView = new UnityEvent();
        }
        GameManager.OnItemInView.AddListener(SetCurrentItemInView);
    }

    private void SetCurrentItemInView()
    {
        GameManager.currentItemInView = selectedItem;
    }
}
