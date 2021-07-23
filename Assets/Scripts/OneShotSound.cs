using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSound : MonoBehaviour
{
    private AudioSource _audioSource;
    private Collider _collider;
    [SerializeField] private bool oneTime = false;
    [SerializeField] private float pauseTime = 5.0f;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
            _collider.enabled = false;

            if (oneTime == false)
            {
                StartCoroutine(Reset());
            }
            else
            {
                Destroy(gameObject, pauseTime); 
            }
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(pauseTime);
        _collider.enabled = true;
    }
}
