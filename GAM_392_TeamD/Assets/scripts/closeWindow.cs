using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeWindow : MonoBehaviour
{

    
    public GameObject Pbar;
    public float UnfillPbar;


    public void CloseAd()
    {
        gameObject.SetActive(false);
    }


    

    public void reduceProgressBar()
    {
        Pbar.GetComponent<ProgressBar>().IncrementDeProgress(UnfillPbar);
    }
}
