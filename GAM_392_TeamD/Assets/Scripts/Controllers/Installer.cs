using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class Installer : MonoBehaviour
{
    public AdvertController gameController;
    public string[] rounds;
    int round_count;
    int round_current = 0;

    public Sprite[] indicatorLevels;
    private List<SpriteRenderer> barSprites = new List<SpriteRenderer>();

    float performance = 1;

    public float time;
    float time_left;
    float display_time;
    private float second = 1.0f;

    Text text_process;
    Text text_rounds;
    Text text_estimate;

    // Start is called before the first frame update
    void Start()
    {
        round_count = rounds.Length;

        time_left = time;
        GameObject progressBar = gameObject.transform.GetChild(3).gameObject;

        // Get the list of CPU bars (And save the SpriteRenderer to edit later)
        for (int i = 0; i < progressBar.transform.childCount; i++)
        {
            barSprites.Add(progressBar.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
            barSprites[i].sprite = indicatorLevels[0];
        }

        text_process = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>();
        text_rounds = gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();
        text_estimate = gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>();

        UpdateDisplay();

    }

    // Update is called once per frame
    public void UpdateInstaller()
    {
        if (round_current != round_count)
        {
            RunTimer();
        }
        else
        {
            gameController.GameWin();
            FindObjectOfType<AudioManager>().Play("InstallComplete");
        }
    }

    void RunTimer()
    {
        //Run through timer
        if (time_left <= 0.0f)
        {
            time_left = time;
            round_current++;
            gameController.UpdateDifficulty(round_current);
            FindObjectOfType<AudioManager>().Play("InstallComplete");
        }
        time_left -= (second * (1 - performance)) * Time.deltaTime;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        text_rounds.text = "Processes: " + round_current + "/" + round_count;
        display_time = time_left / (1 - performance);

        if (round_current != round_count)
        {
            text_process.text = rounds[round_current] + ": " + Mathf.Floor((1 - (time_left / time)) * 100) + "%";

            float percentage = (1 - (time_left / time)) * (barSprites.Count);

            for (int i = 0; i < barSprites.Count; i++)
            {
                barSprites[i].sprite = indicatorLevels[0];
                if (i < Mathf.Round(percentage))
                {
                    barSprites[i].sprite = indicatorLevels[2];
                    if (Mathf.Floor(percentage) < Mathf.Round(percentage) && i + 1 > Mathf.Floor(percentage))
                    {
                        barSprites[i].sprite = indicatorLevels[1];
                    }
                }
            }

            text_estimate.text = "ETA: " + (Mathf.Floor(display_time / 60)) + ":" + LeadingZero(Mathf.Round(display_time % 60)) + " seconds";

        }
        else
        {
            text_estimate.text = "ETA: " + "0:00 seconds";
        }
    }

    public void UpdateUsage(float usage)
    {
        performance = usage;

    }
    
    string LeadingZero(float n)
    {
        return n.ToString().PadLeft(2, '0');

    }
}
