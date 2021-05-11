using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool gameStatus = false;

    void Update()
    {
        if(gameStatus)
        {
            if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    public void Enable(bool status)
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = status;
        gameStatus = status;
        
    }
}
