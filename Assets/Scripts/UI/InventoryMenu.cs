using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject[] Apples;
    [SerializeField] private GameObject[] Batteries;
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
        GameManager.OnItemInView.RemoveListener(CheckInventory);
        GameManager.OnItemInView.AddListener(CheckInventory);
    }
    
}
