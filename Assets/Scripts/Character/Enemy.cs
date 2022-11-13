using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Humanoid
{
 

    protected override void Die()
    {
        Debug.Log(gameObject.name +" died.");
        Destroy(gameObject);
    }
}
