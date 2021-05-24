using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;

public class AdvertController : MonoBehaviour
{
    //Prefab and possible ads
    public GameObject advert;
    public TaskManager taskManager;
    public Installer installer;

    public GameObject[] extraWindows;

    [System.Serializable]
    public struct Adverts
    {
        [SerializeField] public string Text;
        [SerializeField] public Sprite Image;
        [SerializeField] public int Weight;
        [SerializeField] public bool Borderless;
        [SerializeField] public Sprite ChromeImage;
    }
    public Adverts[] AdvertisementList;

    //Timer control
    public float adMinTime;
    public float adMaxTime;
    public float adIncrease;

    private float time;
    private float second = 1.0f;

    private float time_close = 0.5f;
    private float time_incr = 1.0f;

    bool gameEnd = false;

    //Stores game objects
    private List<GameObject> advertisements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        time = UnityEngine.Random.Range(adMinTime, adMaxTime);
        //DisableController();
        for(int i = 0; i < extraWindows.Length; i++)
        {
            extraWindows[i].GetComponentInChildren<DragWindow>().controllerObject = gameObject;
            extraWindows[i].transform.position = new Vector3(extraWindows[i].transform.position.x, extraWindows[i].transform.position.y, 495.0f - ((float)advertisements.Count * 0.5f));
            advertisements.Add(extraWindows[i]);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            //Run through timer
            if (time <= 0.0f)
            {
                //Reset time and crate ad
                time = UnityEngine.Random.Range(adMinTime, adMaxTime);
                CreateAdvert();
            }
            time -= second * Time.deltaTime;
            installer.UpdateInstaller();
        }
        else
        {
            if (advertisements.Count > 0)
            {
                time -= second * Time.deltaTime;
                if (time <= 0.0f)
                {
                    advertisements[advertisements.Count - 1].GetComponentInChildren<ButtonInteraction>().CommitSuicide();
                    time_incr++;
                    time = time_close / time_incr;
                }
            }
            else
            {
                DisableController();
            }
        }
    }

#nullable enable
    public void CreateAdvert(Vector2 location = default(Vector2), Sprite? sprite = null, bool? killMe = false)
    {
        #region Ad Preparation
        //Select random sprite from list
        Adverts selectedAdvert = AdvertisementList[UnityEngine.Random.Range(0, AdvertisementList.Length)];

        //If function is given, set sprite to given
        if (sprite != null)
        {
            selectedAdvert.Image = sprite;
        }

        //Get pixel size of selected sprite
        float x_max = selectedAdvert.Image.rect.width + 2f;
        float y_max = selectedAdvert.Image.rect.height + 11f;

        //Generate random coordinates
        float x_pos = Mathf.Round(UnityEngine.Random.Range(0, 480 - x_max));
        float y_pos = Mathf.Round(UnityEngine.Random.Range(-270 + y_max + 12, 0));

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
        newAdvertisement.GetComponent<CreateWindow>().displaySprite = selectedAdvert.Image;
        newAdvertisement.GetComponent<CreateWindow>().displayText = selectedAdvert.Text;
        newAdvertisement.GetComponent<CreateWindow>().weight = selectedAdvert.Weight;

        newAdvertisement.GetComponent<CreateWindow>().isBorderless = selectedAdvert.Borderless;
        
        newAdvertisement.GetComponent<CreateWindow>().chromeSprite = selectedAdvert.ChromeImage;
        
        newAdvertisement.GetComponent<CreateWindow>().taskManager = taskManager;
        newAdvertisement.GetComponentInChildren<ButtonInteraction>().controllerObject = gameObject;
        newAdvertisement.GetComponentInChildren<DragWindow>().controllerObject = gameObject;

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

    public void UpdateOrder(GameObject reorderedObject)
    {
        int index = advertisements.IndexOf(reorderedObject) + 1;
        //Decreases Z value of all higher-level ads (to never get too close to the camera)
        for (int i = index; i < advertisements.Count; i++)
        {
            advertisements[i].transform.position = new Vector3(advertisements[i].transform.position.x, advertisements[i].transform.position.y, advertisements[i].transform.position.z + 0.5f);
        }
        advertisements.Remove(reorderedObject);
        reorderedObject.transform.position = new Vector3(reorderedObject.transform.position.x, reorderedObject.transform.position.y, 495.0f - ((float)advertisements.Count * 0.5f));
        advertisements.Add(reorderedObject);
    }

    public void GameWin()
    {
        gameEnd = true;
        time = time_close;

        for (int i = 0; i < extraWindows.Length; i++)
        {
            advertisements.Remove(extraWindows[i]);
        }

    }
}
