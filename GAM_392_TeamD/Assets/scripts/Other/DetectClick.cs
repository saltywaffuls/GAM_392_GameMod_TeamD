using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<AudioManager>().Play("MouseClickDown");
        }

        if (Input.GetMouseButtonUp(0))
        {
            FindObjectOfType<AudioManager>().Play("MouseClickUp");
        }
    }
}
