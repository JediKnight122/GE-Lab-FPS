using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public void LockCursor()
    {
        FindObjectOfType<CharacterPlayer>().SwitchCursorLocked();
    }
}
