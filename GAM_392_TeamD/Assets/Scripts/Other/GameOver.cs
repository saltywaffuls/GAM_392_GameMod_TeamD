using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool gameStatus = false;

    private float coolDown = 0.75f;

    void Update()
    {
        if(gameStatus)
        {
            if (Input.anyKeyDown && coolDown <= 0.0f)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            coolDown -= 1.0f * Time.deltaTime;
        }
    }

    public void Enable(bool status)
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = status;
        gameStatus = status;
        
    }
}
