using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public GameObject controllerObject;
    public CreateWindow window;

    //Grab the sprite renderer (to change later)
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    //Change the sprite to the provided one
    public void UpdateAppearance(Sprite sprite)
    {
        spriteRender.sprite = sprite;
    }

    //Destroy the instance
    public void CommitSuicide()
    {
        controllerObject.GetComponent<AdvertController>().RemoveObject(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
        window.UpdateTaskManager();
    }
}
