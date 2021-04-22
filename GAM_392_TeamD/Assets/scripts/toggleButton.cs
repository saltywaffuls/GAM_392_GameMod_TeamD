using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleButton : MonoBehaviour
{
    public Toggle sellectedToggle;
    public GameObject memeryBar;
    public GameObject timer;
    public float fill;
    public float addedtime;

    public void ToggleON()
    {
       if(sellectedToggle.isOn)
        {
            memeryBar.GetComponent<MemoryBar>().IncrementProgress(fill);
            timer.GetComponent<TImer>().addTime += addedtime;
        }
        else
        {
            memeryBar.GetComponent<MemoryBar>().IncrementDeProgress(fill);
            timer.GetComponent<TImer>().addTime -= addedtime;
        }
        
    }
    

    
}
