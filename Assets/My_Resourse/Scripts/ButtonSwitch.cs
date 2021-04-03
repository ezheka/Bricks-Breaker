using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public Sprite spriteOn, spriteOff;
    private void Awake()
    {
        Vibration.Init(); 
        
        if (!GameData.HapticOff)
            GetComponent<Image>().sprite = spriteOn;
        else
            GetComponent<Image>().sprite = spriteOff;        
    }

    public void Switch()
    {
        GameData.HapticOff = !GameData.HapticOff;

        if (!GameData.HapticOff)
        {
            Vibration.Vibrate(100);
            GetComponent<Image>().sprite = spriteOn;
        }
        else
        {
            GetComponent<Image>().sprite = spriteOff;
        }
    }
}
