using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    private RaycastHit _hit;
    [SerializeField] private float maxDistance = 4.0f;
    [SerializeField] private GameObject pickupMessage;
    private TMP_Text pickupMessageText;

    private float _rayDistance;
    private bool _canSeePickup = false;
    private GameObject currentTarget;
    private void Start()
    {
        pickupMessageText = pickupMessage.GetComponentInChildren<TMP_Text>();
        if (GameManager.OnItemInView == null)
        {
            GameManager.OnItemInView = new UnityEvent();
        } 
        GameManager.OnItemInView.AddListener(ProcessPickup);
    }

    void Update()
    {
        _rayDistance = _canSeePickup ? 1000f : maxDistance;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out _hit, _rayDistance))
        {
            _canSeePickup = _hit.collider.CompareTag("Pickup");
            currentTarget = _hit.collider.gameObject;
        }
        pickupMessage.gameObject.SetActive(_canSeePickup);
        GameManager.ItemInView = _canSeePickup;      
    }

    private bool CheckCanPickUp(GameObject target)
    {
        bool isOnHand = false;
        switch (target.GetComponent<Item>().selectedItem)
        {
            case Item.ItemType.APPLE:
                isOnHand = GameManager.Apples < 6;
                break;
            case Item.ItemType.AMMO:
                break;
            case Item.ItemType.BATTERY:
                isOnHand = GameManager.Batteries < 4;
                break;
            case Item.ItemType.AXE:
                isOnHand = !GameManager.availableWeapons.Contains(Item.ItemType.AXE);
                break;
            case Item.ItemType.BAT:
                isOnHand = !GameManager.availableWeapons.Contains(Item.ItemType.BAT);
                break;
            case Item.ItemType.KNIFE:
                isOnHand = !GameManager.availableWeapons.Contains(Item.ItemType.KNIFE);
                break;
            case Item.ItemType.HANDGUN:
                isOnHand = !GameManager.availableWeapons.Contains(Item.ItemType.HANDGUN);
                break;
            case Item.ItemType.CROSSBOW:
                isOnHand = !GameManager.availableWeapons.Contains(Item.ItemType.CROSSBOW);
                break;
        }
        return isOnHand;
    }
    private void ProcessPickup()
    {
        pickupMessageText.text = CheckCanPickUp(currentTarget) ? "Press 'E' to pickup" : "I have enough of those";
        if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            switch (currentTarget.GetComponent<Item>().selectedItem)
            {
                case Item.ItemType.APPLE:
                    if (CheckCanPickUp(currentTarget))
                    {
                        GameManager.Apples += GameManager.Apples < 6 ? 1 : 0;
                        AudioManager.Instance.pickupSounds[0].Play();
                        Destroy(currentTarget);
                    }
                    break;
                case Item.ItemType.AMMO:
                    if (CheckCanPickUp(currentTarget))
                    {
                        Debug.Log("Its some Ammo!");
                        Destroy(currentTarget);
                    }
                    break;
                case Item.ItemType.BATTERY:
                    if (CheckCanPickUp(currentTarget))
                    {
                        GameManager.Batteries += GameManager.Batteries < 4 ? 1 : 0;
                        AudioManager.Instance.pickupSounds[1].Play();
                        Destroy(currentTarget);
                    }
                    break;
                case Item.ItemType.AXE:
                    if (!GameManager.availableWeapons.Contains(currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(currentTarget); 
                    }
                    break;
                case Item.ItemType.BAT:
                    if (!GameManager.availableWeapons.Contains(currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(currentTarget); 
                    }
                    break;
                case Item.ItemType.KNIFE:
                    if (!GameManager.availableWeapons.Contains(currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(currentTarget); 
                    }
                    break;
                case Item.ItemType.HANDGUN:
                    if (!GameManager.availableWeapons.Contains(currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(currentTarget); 
                    }
                    break;
                case Item.ItemType.CROSSBOW:
                    if (!GameManager.availableWeapons.Contains(currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(currentTarget); 
                    }
                    break;
            }
            
            GameManager.ItemInView = false;
        }
    }
}
 