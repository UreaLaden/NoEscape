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
        switch (target.GetComponent<Item>().selectedItem)
        {
            case Item.ItemType.APPLE:
                return GameManager.Apples < 6;
            case Item.ItemType.AMMO:
                break;
            case Item.ItemType.BATTERY:
                return GameManager.Batteries < 4;
        }
        return false;
    }
    private void ProcessPickup()
    {
        pickupMessageText.text = CheckCanPickUp(currentTarget) ? "Press 'E' to pickup" : "I have enough of those";
        if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            switch (currentTarget.GetComponent<Item>().selectedItem)
            {
                case Item.ItemType.APPLE:
                    GameManager.Apples += GameManager.Apples < 6 ? 1 : 0;
                    AudioManager.Instance.pickupSounds[0].Play();
                    if (GameManager.canPickupApple)
                    {
                        Destroy(_hit.transform.gameObject);
                    }
                    break;
                case Item.ItemType.AMMO:
                    Debug.Log("Its some Ammo!");
                    break;
                case Item.ItemType.BATTERY:
                    GameManager.Batteries += GameManager.Batteries < 4 ? 1 : 0;
                    AudioManager.Instance.pickupSounds[1].Play();
                    if (GameManager.canPickupBattery)
                    {
                        Destroy(_hit.transform.gameObject);
                    }
                    break;
            }
            GameManager.ItemInView = false;
        }
    }
}
 