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
        pickupMessageText.text = GameManager.canPickupApple ? "Press 'E' to pickup" : "I have enough of those";
        _rayDistance = _canSeePickup ? 1000f : maxDistance;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out _hit, _rayDistance))
        {
            _canSeePickup = _hit.collider.CompareTag("Pickup");
        }
        pickupMessage.gameObject.SetActive(_canSeePickup);
        GameManager.ItemInView = _canSeePickup;      
    }

    private void ProcessPickup()
    {
        if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            switch (GameManager.currentItemInView)
            {
                case Item.ItemType.APPLE:
                    GameManager.Apples += GameManager.Apples < 6 ? 1 : 0;
                    if (GameManager.canPickupApple)
                    {
                        Destroy(_hit.transform.gameObject);
                    }
                    GameManager.ItemInView = false;
                    break;
                case Item.ItemType.AMMO:
                    Debug.Log("Its some Ammo!");
                    break;
            }
        }
    }
}
 