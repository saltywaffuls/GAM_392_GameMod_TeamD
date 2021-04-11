using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;

    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    public GameObject endScreen;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }


    // Start is called before the first frame update
    void Start()
    {
        endScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;

        if (slider.value > targetProgress)
            slider.value -= fillSpeed * Time.deltaTime;

        if(targetProgress >= 0.9)
        {
            endScreen.SetActive(true);

            Debug.Log("game over");
        }

    }

    //add progress to the bar
    public void IncrementProgress(float newProgress){

        targetProgress = slider.value + newProgress;
        }

    //sub progress to the bar
    public void IncrementDeProgress(float newProgress)
    {

        targetProgress = slider.value - newProgress;
    }

}


