using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIRangeDetection : MonoBehaviour
{
    private SphereCollider collider;

    public event Action TriggerEntered;
    public event Action TriggerExited;
    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    public void SetColliderRadius(float pRadius)
    {
        collider.radius = pRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExited?.Invoke();
    }
}
