using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    [SerializeField] private float captureTime;

    private bool playerInsideZone = false;
    private bool enemyInsideZone = false;
    private float currentCaptureProgression = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInsideZone && !enemyInsideZone)
        {
            currentCaptureProgression += Time.deltaTime;
            if(currentCaptureProgression>=captureTime) ZoneCaptured();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entering Trigger...");
        if (collider.gameObject.tag.Equals("Player"))
        {
            playerInsideZone = true;
        }
        
        if (collider.gameObject.tag.Equals("Enemy"))
        {
            enemyInsideZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInsideZone = false;
        }
        
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemyInsideZone = false;
        }
    }

    private void ZoneCaptured()
    {
        Debug.Log("A zone was captured.");
        this.enabled = false;
    }
}
