using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource spawnSound;
    [SerializeField] private AudioSource despawnSound;
    [SerializeField] private AudioSource hitSound;
    // Start is called before the first frame update

    private void Start()
    {
        GetComponent<Health>().OnHealthDepleted += OnDeath;
    }

    private void OnEnable()
    {
        spawnSound.Play();
    }

    private void OnDeath()
    {
        despawnSound.Play();
    }

    public void PlayHitSound()
    {
     hitSound.Play();
    }
}
