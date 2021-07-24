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
    GameManager.OnConsumeItem.AddListener(RestoreHealth);
    GameManager.OnHealthChanged.AddListener(UpdateHealth);
  }

  private void Update()
  {
    if (GameManager.PlayerHealth > 100) { GameManager.PlayerHealth = 100; }
    if (GameManager.PlayerHealth < 0) { GameManager.PlayerHealth = 0; }
  }

  private void UpdateHealth()
  {
    playerHealthText.text = GameManager.PlayerHealth + "%";
    GameManager.PlayerHealthChanged = false;
    GameManager.OnHealthChanged.RemoveListener(UpdateHealth);
    GameManager.OnHealthChanged.AddListener(UpdateHealth);
  }
  
  private void RestoreHealth()
  {
      GameManager.amountToRestore = GameManager.PlayerHealth < 100 && GameManager.PlayerHealth >= 0 ? GameManager.amountToRestore : 0;
      GameManager.Apples--;
      GameManager.PlayerHealth += GameManager.amountToRestore;
      GameManager.PlayerHealthChanged = true;
      GameManager.ItemConsumed = false;
      GameManager.OnConsumeItem.RemoveListener(RestoreHealth);
      GameManager.OnConsumeItem.AddListener(RestoreHealth);
  }
}
