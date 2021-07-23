using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField] private TMP_Text playerHealthText;

  private void Start()
  {
    playerHealthText.text = GameManager.PlayerHealth + "%";
    if (GameManager.OnHealthChanged == null)
    {
      GameManager.OnHealthChanged = new UnityEvent();
    }
    GameManager.OnHealthChanged.AddListener(UpdateHealth);
  }

  private void UpdateHealth()
  {
    Debug.Log("Updating Health");
    playerHealthText.text = GameManager.PlayerHealth + "%";
    GameManager.PlayerHealthChanged = false;
  }
}
