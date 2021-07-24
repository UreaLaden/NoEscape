using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] public AudioSource[] pickupSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
