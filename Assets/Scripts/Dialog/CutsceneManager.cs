using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float cutsceneLenght = 120;
    private void Update()
    {
        cutsceneLenght -= Time.deltaTime;
        if(cutsceneLenght<=0) EndCutscene();
    }

    public void EndCutscene(){
      gameObject.SetActive(false);
    }
}
