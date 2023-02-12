using System.Collections;
using System.Collections.Generic;
using GameMode;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void CallToLoadMainMenu()
    {
    GameManager.instance.LoadMainMenu();
    Debug.Log("Returning to main menu...");
    }
}
