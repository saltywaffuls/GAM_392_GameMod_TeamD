using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class CreateWindow : MonoBehaviour
{
    public TaskManager taskManager;
    public Sprite displaySprite;
    public string displayText;
    public int weight;

    // Start is called before the first frame update
    void Start()
    {
        #region Set Ad Appearance
        //Grab child (advertisement)
        GameObject advert = gameObject.transform.GetChild(2).gameObject;
        SpriteRenderer advertRender = advert.GetComponent<SpriteRenderer>();

        //Increase impact on CPU
        taskManager.ModifyCpu(weight);

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

        #region Set Button UI size (Window Title Section)
        //Modify the canvas of the title bar
        GameObject canvasTitle = window.transform.GetChild(0).gameObject;
        canvasTitle.GetComponent<RectTransform>().sizeDelta = new Vector3(x_size - 13.0f, 8.0f, 0.0f);
        canvasTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2((x_size - 13.0f) * 0.5f, -4.0f);

        //Modify the button of the title bar
        GameObject canvasTitleButton = canvasTitle.transform.GetChild(0).gameObject;
        canvasTitleButton.GetComponent<RectTransform>().sizeDelta = new Vector3(x_size - 13.0f, 8.0f, 0.0f);
        
        GameObject canvasTitleText = canvasTitleButton.transform.GetChild(0).gameObject;
        canvasTitleText.GetComponent<RectTransform>().sizeDelta = new Vector3(x_size - 13.0f, 10.0f, 0.0f);
        canvasTitleText.GetComponent<UnityEngine.UI.Text>().text = displayText;
        #endregion

        #region Set Button UI size (Window Ad Section)
        //Modify the canvas of the ad display
        GameObject canvasAd = window.transform.GetChild(1).gameObject;
        canvasAd.GetComponent<RectTransform>().sizeDelta = new Vector3(x_size, y_size - 8.0f, 0.0f);
        canvasAd.GetComponent<RectTransform>().anchoredPosition = new Vector2(x_size * 0.5f, (y_size + 8.0f) * -0.5f);

        //Modify the button of the ad display
        GameObject canvasAdButton = canvasAd.transform.GetChild(0).gameObject;
        canvasAdButton.GetComponent<RectTransform>().sizeDelta = new Vector3(x_size, y_size - 8.0f, 0.0f);
        #endregion

        #region Set Button UI Position (Window X Section)
        //Grab child (button)
        GameObject x_button = gameObject.transform.GetChild(1).gameObject;
        
        //Update position of button to end of window
        x_button.transform.position = window.transform.position + new Vector3(x_size - 13.0f, 0.0f, -0.1f);
        #endregion
    }

    public void UpdateTaskManager()
    {
        taskManager.ModifyCpu(-weight);
    }
}
