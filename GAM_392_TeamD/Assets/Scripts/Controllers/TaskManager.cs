using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject controller;
    public GameOver gameOver;
    
    public float maxCpu;
    public float maxRam;

    private float currentCpu = 0.0f;
    private float currentRam = 0.0f;
    private int barAmount = 14;

    private int tabSelected = 0;

    public Sprite[] indicatorLevels;
    public Sprite[] windowTabs;

    private List<SpriteRenderer> cpuSprites = new List<SpriteRenderer>();
    private List<SpriteRenderer> ramSprites = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        // Get the text and button info
        GameObject tabList = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        GameObject tabPerformance = tabList.transform.GetChild(0).gameObject;
        GameObject tabProcesses = tabList.transform.GetChild(1).gameObject;
        UpdateTabs(true, tabPerformance);
        UpdateTabs(false, tabProcesses);

        // Get child objects of the task manager (to get the bars)
        GameObject performanceTab = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
        GameObject cpuBars = performanceTab.gameObject.transform.GetChild(3).gameObject;
        GameObject ramBars = performanceTab.gameObject.transform.GetChild(4).gameObject;

        // Get the list of CPU bars (And save the SpriteRenderer to edit later)
        for (int i = 0; i < cpuBars.transform.childCount; i++)
        {
            cpuSprites.Add(cpuBars.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
            cpuSprites[i].sprite = indicatorLevels[0];
        }
        // Get the list of Memory Bars (And save the SpriteRenderer to edit later)
        for (int i = 0; i < ramBars.transform.childCount; i++)
        {
            ramSprites.Add(ramBars.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
            ramSprites[i].sprite = indicatorLevels[0];
        }
    }

    void Update()
    {
        //ModifyCpu(10.0f * Time.deltaTime);
        //ModifyRam(10.0f * Time.deltaTime);
    }

    private void UpdateTabs(bool enabled, GameObject tabs)
    {
        GameObject text = tabs.transform.GetChild(1).gameObject;
        if(!enabled)
        {
            SpriteRenderer tabSprite = tabs.GetComponent<SpriteRenderer>();
            tabSprite.sprite = windowTabs[1];
            text.GetComponent<UnityEngine.UI.Text>().color = new Color(0.278f, 0.278f, 0.278f, 1.0f);
        }
    }

    public void ModifyCpu(float deltaCpu)
    {
        currentCpu += deltaCpu;
        float cpuUsage = (currentCpu / maxCpu) * (barAmount / 2);

        for (int i = 0; i < cpuSprites.Count; i++)
        {
            cpuSprites[i].sprite = indicatorLevels[0];
            if (i < Mathf.Round(cpuUsage))
            {
                cpuSprites[i].sprite = indicatorLevels[2];
                if (Mathf.Floor(cpuUsage) < Mathf.Round(cpuUsage) && i + 1 > Mathf.Floor(cpuUsage))
                {
                    cpuSprites[i].sprite = indicatorLevels[1];
                }
                UnityEngine.Debug.Log(indicatorLevels[1]);
            }
        }
        if(currentCpu / maxCpu >= 1)
        {
            CrashGame();
        }
    }

    public void ModifyRam(float deltaRam)
    {
        currentRam += deltaRam;
        float ramUsage = (currentRam / maxRam) * (barAmount / 2);

        for (int i = 0; i < ramSprites.Count; i++)
        {
            cpuSprites[i].sprite = indicatorLevels[0];
            if (i < Mathf.Round(ramUsage))
            {
                ramSprites[i].sprite = indicatorLevels[2];
                if (Mathf.Floor(ramUsage) < Mathf.Round(ramUsage) && i + 1 > Mathf.Floor(ramUsage))
                {
                    ramSprites[i].sprite = indicatorLevels[1];
                }
            }
        }
        if (currentRam / maxRam >= 1)
        {
            CrashGame();
        }
    }

    private void CrashGame()
    {
        AdvertController gameController = controller.GetComponent<AdvertController>();
        gameController.DisableController();
        gameOver.Enable(true);
    }
}
