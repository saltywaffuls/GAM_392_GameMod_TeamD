using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImer : MonoBehaviour
{

    float timer = 0;
    public Text myText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        changeText();
    }

    public void changeText()
    {
        myText.text = "time" + timer;
    }
}
