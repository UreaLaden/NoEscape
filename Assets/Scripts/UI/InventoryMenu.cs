using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject[] Apples;
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

    // Update is called once per frame
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
    }
}
