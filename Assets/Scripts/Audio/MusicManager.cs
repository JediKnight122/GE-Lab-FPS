using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour

{ 
    
    public static MusicManager instance;
    
    [SerializeField] private AudioSource[] actionLvl;
    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
        ChangeActionLevel(0);
    }

   
    public void ChangeActionLevel(int pActionLevel)
    {
        pActionLevel++;
        
        Debug.Log("Setting new Action Level to: "+pActionLevel);
        
        for (int i = 0; i <= actionLvl.Length-1; i++)
        {
            Debug.Log("Adjusting Volume of  "+i+" to "+ (i<=pActionLevel));
            actionLvl[i].volume = Convert.ToInt32(i<=pActionLevel);
        }
    }
}
