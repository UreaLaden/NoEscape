using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public static Ammo Instance;
    [SerializeField] private int clipSize = 15;
    [SerializeField] private int shotsFired = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        GameManager.remainingShots = clipSize - shotsFired;
        if (GameManager.remainingShots > 0)
        {
            if (Input.GetMouseButtonDown(0) && PlayerInput.Instance.isAiming)
            {
                Shoot();
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (GameManager.Ammo > 0 && GameManager.remainingShots < clipSize)
        {
            //Every 15 shots fire blank until player reloads with 'R'
            GameManager.remainingShots = GameManager.Ammo * clipSize;
            shotsFired = 0;
        }
        // Let play know to check they're mag
    }

    private void Shoot()
    {
        Debug.Log("Bang! Bang!");
        shotsFired++;
    }
}
