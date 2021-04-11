using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeWindow : MonoBehaviour
{

    public GameObject ad;
    public GameObject Pbar;
    public float UnfillPbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void close()
    {
        ad.SetActive(false);
       
    }

    public void reduceProgressBar()
    {
        Pbar.GetComponent<ProgressBar>().IncrementDeProgress(UnfillPbar);
    }
}
