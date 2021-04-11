using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImer : MonoBehaviour
{

    float Currenttime = 0f;
    public float addTime = 1;
    public float StartTime = 15f;
    public GameObject victoryScreen;

    [SerializeField] Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        Currenttime = StartTime;
        victoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Currenttime -= addTime * Time.deltaTime;
        timerText.text = Currenttime.ToString("0");

        if ( Currenttime <= 0)
        {
            victoryScreen.SetActive(true);
        }

    }

    

}
