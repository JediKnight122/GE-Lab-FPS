using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.UI;

public class Button_Behavior : MonoBehaviour
{
    
    public void ButtonHover()
    {
        AudioManager.instance.Play("Button_Hover");
    }

    public void ButtonSelect()
    {
        AudioManager.instance.Play("Button_Select");
    }

    public void UIConfirmSound()
    {
       AudioManager.instance.Play("UI_Confirm");
    }
    public void TurnGreen(RawImage image)
    {
        image.color = Color.green;

    }
    public void TurnNormalColor(RawImage image)
    {
        image.color = Color.white;
    }
}
    
