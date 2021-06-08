using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    public GameObject glitch;
    public Sprite[] sprites;

    public float lifeMin;
    public float lifeMax;

    public float freqMin;
    public float freqMax;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        time = UnityEngine.Random.Range(freqMin, freqMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            float life = UnityEngine.Random.Range(lifeMin, lifeMax);
            GameObject newGlitch = Instantiate(glitch, new Vector3(0, 0, 1), Quaternion.identity);
            newGlitch.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
            Destroy(newGlitch, life);
            time = UnityEngine.Random.Range(freqMin, freqMax);
            //FindObjectOfType<AudioManager>().Play("glitch_" + UnityEngine.Random.Range(0, 2));
        }

        time -= 1.0f * Time.deltaTime;
        
    }
    
}
