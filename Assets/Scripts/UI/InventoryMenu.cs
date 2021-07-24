using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Effects;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject[] Apples;
    [SerializeField] private GameObject[] Batteries;
    [SerializeField] private GameObject[] Weapons;
    [SerializeField] private GameObject[] Ammo;
    [SerializeField] private GameObject[] Bolts;
    [SerializeField] private GameObject[] Keys;

    [SerializeField] private List<Item.ItemType> onHandKeys;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        CheckInventory();
        
        if (GameManager.OnItemInView == null)
        {
            GameManager.OnItemInView = new UnityEvent();
        }
        GameManager.OnItemInView.AddListener(CheckInventory);
    }

    void Update()
    {
        onHandKeys = GameManager.availableKeys;
        inventoryPanel.SetActive(GameManager.isPaused);
        Time.timeScale = GameManager.isPaused ? 0 : 1;
        Cursor.visible = GameManager.isPaused;
        if (Input.GetKeyUp(KeyCode.I))
        {
            GameManager.isPaused = !GameManager.isPaused;
        }
    }

   
    private void CheckInventory()
    {
        for (int i = 0; i < Apples.Length; i++)
        {
            Apples[i].SetActive(i < GameManager.Apples);
        }

        for (int j = 0; j < Batteries.Length; j++)
        {
            Batteries[j].SetActive(j < GameManager.Batteries);
        }

        for (int k = 0; k < Weapons.Length; k++)
        {
            Item.ItemType weaponType = Weapons[k].GetComponent<Item>().selectedItem;
            Weapons[k].SetActive(GameManager.availableWeapons.Contains(weaponType));
        }

        for (int l = 0; l < Ammo.Length; l++)
        {
            Ammo[l].SetActive(l < GameManager.Ammo);
        }

        for (int m = 0; m < Bolts.Length; m++)
        {
            Bolts[m].SetActive(m < GameManager.Bolts);
        }

        for (int n = 0; n < Keys.Length; n++)
        {
            Item.ItemType keyType = Keys[n].GetComponent<Item>().selectedItem;
            Keys[n].SetActive(GameManager.availableKeys.Contains(keyType));
        }
        GameManager.OnItemInView.RemoveListener(CheckInventory);
        GameManager.OnItemInView.AddListener(CheckInventory);
    }
    
}
