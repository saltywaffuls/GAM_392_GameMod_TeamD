using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUPControler : MonoBehaviour
{


    
    public GameObject prefab;
    public GameObject Pbar;
    public float fill;

    
    public float speed;
    public bool timeRunning;

    // Start is called before the first frame update
    void Start()
    {
        
         prefab.SetActive(false);


        timeRunning = true;
    }

  

    // Update is called once per frame
    void Update()
    {

        if (timeRunning)
        {
            if (speed >= 0)
            {
                speed -= Time.deltaTime;
            }
            else
            {

                prefab.SetActive(true);
                speed = 0;
                timeRunning = false;
                Pbar.GetComponent<ProgressBar>().IncrementProgress(fill);


            }
        }

       
      

    }

   
}
