using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUPControler : MonoBehaviour
{


    public Transform[] adPoints;
    public GameObject [] ad;
    public GameObject Pbar;
    public float fillPbar;
    



    int randomAdpoint, RandomAd;
    public static bool adsON;

    // Start is called before the first frame update
    void Start()
    {
        adsON = true;
        InvokeRepeating("RandomPopUpAd", 0f, 1f);
  
       
    }


   
      void RandomPopUpAd()
    {


        if (adsON)
        {
            Debug.Log("random");
            randomAdpoint = Random.Range(0, adPoints.Length);
            RandomAd = Random.Range(0, ad.Length);
            Instantiate(ad[RandomAd], adPoints[randomAdpoint].position, Quaternion.identity);
            Pbar.GetComponent<ProgressBar>().IncrementProgress(fillPbar);
        }
        
    }

    

    


}
