using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class VoicelineManager : MonoBehaviour
{
    public static VoicelineManager instance;
    
    [SerializeField] private RandomAudioListPlayer deathVoicelines;

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
    }
    
    public void PlayPlayerDeathVoiceline()
    {
        deathVoicelines.Play();
    }
}
