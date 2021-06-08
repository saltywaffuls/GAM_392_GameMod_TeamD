using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvertController : MonoBehaviour
{
    //Prefab and possible ads
    public GameObject advert;
    public TaskManager taskManager;
    public Installer installer;

    //public GameObject[] extraWindows;

    [System.Serializable]
    public struct Rounds
    {
        public float adMinTime;
        public float adMaxTime;

        [System.Serializable]
        public struct Adverts
        {
            public string Text;
            public Sprite Image;
            public int Weight;
            public bool Borderless;
            public Sprite ChromeImage;
        }
        public Adverts[] AdvertisementList;
    }
    public Rounds[] RoundList;

    private int round_current = 0;

    //Timer control
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
        time = UnityEngine.Random.Range(RoundList[round_current].adMinTime, RoundList[round_current].adMaxTime);
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
                time = UnityEngine.Random.Range(RoundList[round_current].adMinTime, RoundList[round_current].adMaxTime);
                //Select random sprite from list
                int selAd = UnityEngine.Random.Range(0, RoundList[round_current].AdvertisementList.Length);
                CreateAdvert(default(Vector2), false, RoundList[round_current].AdvertisementList[selAd].Image, RoundList[round_current].AdvertisementList[selAd].Borderless, RoundList[round_current].AdvertisementList[selAd].Text, RoundList[round_current].AdvertisementList[selAd].Weight, RoundList[round_current].AdvertisementList[selAd].ChromeImage);
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                DisableController();
            }
        }
    }

#nullable enable
    public void CreateAdvert(Vector2 location, bool setLocation, Sprite sprite, bool borderless, String text, int weight, Sprite chrome)
    {
        #region Ad Preparation
        //Select random sprite from list

        //Get pixel size of selected sprite
        float x_max = sprite.rect.width + 2f;
        float y_max = sprite.rect.height + 11f;

        //Generate random coordinates
        float x_pos = Mathf.Round(UnityEngine.Random.Range(100, 480 - x_max));
        float y_pos = Mathf.Round(UnityEngine.Random.Range(-270 + y_max + 12, 0));

        //If location is given, set the coords to that
        if (setLocation == true)
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
        newAdvertisement.GetComponent<CreateWindow>().displaySprite = sprite;
        newAdvertisement.GetComponent<CreateWindow>().displayText = text;
        newAdvertisement.GetComponent<CreateWindow>().weight = weight;

        newAdvertisement.GetComponent<CreateWindow>().isBorderless = borderless;

        newAdvertisement.GetComponent<CreateWindow>().chromeSprite = chrome;

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
        /*
        for (int i = 0; i < extraWindows.Length; i++)
        {
            advertisements.Remove(extraWindows[i]);
        }
        */
    }

    public void UpdateDifficulty(int round)
    {
        round_current = round;
    }
}
