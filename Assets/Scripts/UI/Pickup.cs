using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float maxDistance = 4.0f;
    [SerializeField] private GameObject pickupMessage;
    private TMP_Text _pickupMessageText;
    private float _rayDistance;
    private bool _canSeePickup = false;
    private GameObject _currentTarget;
    private void Start()
    {
        _pickupMessageText = pickupMessage.GetComponentInChildren<TMP_Text>();
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
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
        {
            _canSeePickup = hit.collider.CompareTag("Pickup") || hit.collider.CompareTag("Weapon") || hit.collider.CompareTag("Key");
            _currentTarget = hit.collider.gameObject;
        }
        pickupMessage.gameObject.SetActive(_canSeePickup);
        GameManager.ItemInView = _canSeePickup;      
    }

    private bool CheckCanPickUp(GameObject target)
    {
        bool isOnHand = false;
        switch (target.tag)
        {
            case "Pickup":
                switch (target.GetComponent<Item>().selectedItem)
                {
                    case Item.ItemType.APPLE:
                        isOnHand = GameManager.Apples < 6;
                        break;
                    case Item.ItemType.AMMO:
                        isOnHand = GameManager.Ammo < 3;
                        break;
                    case Item.ItemType.BATTERY:
                        isOnHand = GameManager.Batteries < 4;
                        break; 
                    case Item.ItemType.BOLT:
                        isOnHand = GameManager.Bolts < 2;
                        break;
                }
                break;
            case "Weapon":
                isOnHand = !GameManager.availableWeapons.Contains(_currentTarget.GetComponent<Item>().selectedItem);
                break;
            case "Key":
                isOnHand = !GameManager.availableKeys.Contains(_currentTarget.GetComponent<Item>().selectedItem);
                break;
        }
        return isOnHand;
    }
    private void ProcessPickup()
    {
        
        _pickupMessageText.text = CheckCanPickUp(_currentTarget) ? "Press 'E' to pickup" : "I have enough of those";
        if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            switch (_currentTarget.tag)
            {
                case "Pickup":
                    switch (_currentTarget.GetComponent<Item>().selectedItem)
                    {
                        case Item.ItemType.APPLE:
                            Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                            if (CheckCanPickUp(_currentTarget))
                            {
                                GameManager.Apples += GameManager.Apples < 6 ? 1 : 0;
                                AudioManager.Instance.pickupSounds[0].Play();
                                Destroy(_currentTarget);
                            }
                            break;
                        case Item.ItemType.AMMO:
                            Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                            if (CheckCanPickUp(_currentTarget))
                            {
                                GameManager.Ammo += GameManager.Ammo < 3 ? 1 : 0;
                                Destroy(_currentTarget);
                            }
                            break;
                        case Item.ItemType.BOLT:
                            Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                            if (CheckCanPickUp(_currentTarget))
                            {
                                GameManager.Bolts += GameManager.Bolts < 2 ? 1 : 0;
                                Destroy(_currentTarget);
                            }
                            break;
                        case Item.ItemType.BATTERY:
                            Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                            if (CheckCanPickUp(_currentTarget))
                            {
                                GameManager.Batteries += GameManager.Batteries < 4 ? 1 : 0;
                                AudioManager.Instance.pickupSounds[1].Play();
                                Destroy(_currentTarget);
                            }
                            break;
                    }
                    break;
                case "Weapon":
                    Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                    if (!GameManager.availableWeapons.Contains(_currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableWeapons.Add(_currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(_currentTarget); 
                    }
                    break;
                case "Key":
                    Debug.Log("Collected a " + _currentTarget.GetComponent<Item>().selectedItem);
                    if (!GameManager.availableKeys.Contains(_currentTarget.GetComponent<Item>().selectedItem))
                    {
                        GameManager.availableKeys.Add(_currentTarget.GetComponent<Item>().selectedItem);
                        Destroy(_currentTarget); 
                    }
                    break;
            }
            GameManager.ItemInView = false;
        }
    }
}
 