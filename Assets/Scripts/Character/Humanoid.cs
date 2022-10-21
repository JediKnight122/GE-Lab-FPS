using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour
{
  protected abstract void Die();

  protected void OnEnable()
  {
    Debug.Log("Subscribing to Death Event");
    GetComponent<Health>().OnHealthDepleted += Die;
  }

  
  }
