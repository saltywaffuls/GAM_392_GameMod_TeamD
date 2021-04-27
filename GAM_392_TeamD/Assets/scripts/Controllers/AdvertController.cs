using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class AdvertController : MonoBehaviour
{
    //Prefab and possible ads
    public GameObject advert;
    public Sprite[] sprites;

    //Timer control
    public float adMinTime;
    public float adMaxTime;
    public float adIncrease;

    private float time;
    private float second = 1.0f;

    //Stores game objects
    private List<GameObject> advertisements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        time = UnityEngine.Random.Range(adMinTime, adMaxTime);
        //DisableController();
    }

    // Update is called once per frame
    void Update()
    {
        //Run through timer
        if (time <= 0.0f)
        {
            //Reset time and crate ad
            time = UnityEngine.Random.Range(adMinTime, adMaxTime);
            CreateAdvert();
        }
        time -= second * Time.deltaTime;
    }
    
    #nullable enable
    public void CreateAdvert(Vector2 location = default(Vector2), Sprite? sprite = null, bool? killMe = false)
    {
        #region Ad Preparation
        //Select random sprite from list
        Sprite selectedSprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        //If function is given, set sprite to given
        if(sprite != null)
        {
            selectedSprite = sprite;
        }
 
        //Get pixel size of selected sprite
        float x_max = selectedSprite.rect.width + 2f;
        float y_max = selectedSprite.rect.height + 11f;

        //Generate random coordinates
        float x_pos = Mathf.Round(UnityEngine.Random.Range(0, 480 - x_max));
        float y_pos = Mathf.Round(UnityEngine.Random.Range(-270 + y_max, 0));

        //If location is given, set the coords to that
        if (killMe == true)
        {
            x_pos = location.x;
            y_pos = location.y;
        }
    #nullable disable

        //Generate depth based on last entry in list
        float z_pos = 495f - ((float)advertisements.Count * 0.5f);
        #endregion

        #region Create Ad
        //Create new advertisement
        GameObject newAdvertisement = Instantiate(advert, new Vector3(x_pos, y_pos, z_pos), Quaternion.identity);
        newAdvertisement.GetComponent<CreateWindow>().displaySprite = selectedSprite;
        newAdvertisement.GetComponentInChildren<ButtonInteraction>().controllerObject = gameObject;

       //Add advertisement to list
        advertisements.Add(newAdvertisement);
        #endregion
    }

    public void DisableController()
    {
        //Disable GameObject (Ads will no longer generate)
        gameObject.SetActive(false);
    }

    public void RemoveObject(GameObject destroyedObject)
    {
        int index = advertisements.IndexOf(destroyedObject) + 1;
        //Decreases Z value of all higher-level ads (to never get too close to the camera)
        for (int i = index; i < advertisements.Count; i++)
        {
            advertisements[i].transform.position = new Vector3(advertisements[i].transform.position.x, advertisements[i].transform.position.y, advertisements[i].transform.position.z + 0.5f);
        }
        advertisements.Remove(destroyedObject);
    }
}
