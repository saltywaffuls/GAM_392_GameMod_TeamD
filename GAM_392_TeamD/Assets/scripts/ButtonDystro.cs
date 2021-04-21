using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDystro : MonoBehaviour

{
    GameObject pbar;
    public GameObject obj;
    public float reducePbar;

    void Start()
    {
        pbar = GameObject.Find("Progress Bar");

    }

    public void DytroPopUP()
    {
        Destroy(obj);
    }

    public void reduceProgressBar()
    {
        pbar.GetComponent<ProgressBar>().IncrementDeProgress(reducePbar);
    }
}
