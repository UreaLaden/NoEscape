using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHRotator : MonoBehaviour
{
    [SerializeField] private GameObject LHObject;

    [SerializeField] private float rotationSpeed = 0.1f;

    void Update()
    {
        LHObject.transform.Rotate(0.0f,rotationSpeed,0.0f, Space.World);
    }
}
