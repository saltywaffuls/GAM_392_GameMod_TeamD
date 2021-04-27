using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreateWindow : MonoBehaviour
{
    public Sprite displaySprite;

    // Start is called before the first frame update
    void Start()
    {
        #region Set Ad Appearance
        //Grab child (advertisement)
        GameObject advert = gameObject.transform.GetChild(2).gameObject;
        SpriteRenderer advertRender = advert.GetComponent<SpriteRenderer>();
        
        //Set sprite
        advertRender.sprite = displaySprite;

        //Get the size of the image (in pixels) (inlcudes borders of window)
        float x_size = advertRender.sprite.rect.width + 2f;
        float y_size = advertRender.sprite.rect.height + 11f;
        #endregion

        #region Set Window Size
        //Grab child (window border)
        GameObject window = gameObject.transform.GetChild(0).gameObject;
        
        //Set 9-slice size (in draw mode) to increase size
        SpriteRenderer window_sprite = window.GetComponent<SpriteRenderer>();
        window_sprite.size = new Vector2(x_size, y_size);
        #endregion

        #region Set X Button Position
        //Grab child (button)
        GameObject x_button = gameObject.transform.GetChild(1).gameObject;
        
        //Update position of button to end of window
        x_button.transform.position = window.transform.position + new Vector3(x_size - 13.0f, 0.0f, -0.1f);
        #endregion
    }
}
