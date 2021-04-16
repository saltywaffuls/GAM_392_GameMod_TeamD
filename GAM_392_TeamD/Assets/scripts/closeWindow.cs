using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeWindow : MonoBehaviour
{

    public Texture btnTexture;
    public GameObject Pbar;
    public float UnfillPbar;

    
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), btnTexture))
            Debug.Log("close ad");
        
        if (GUI.Button(new Rect(10, 70, 50, 30), "X"))
            Debug.Log("close ad");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
            reduceProgressBar();
    }

    

    public void reduceProgressBar()
    {
        Pbar.GetComponent<ProgressBar>().IncrementDeProgress(UnfillPbar);
    }
}
