using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public static Weather instance;
    private string DayColorCode;

    public Sprite MoonSprite;
    private Sprite SunSprite;

    private SpriteRenderer BackGround;
    private SpriteRenderer Sun;

    private Transform Decorations;

    private bool Night;
    private bool TimersUp;
    public float currentRGB;


    private void Start()
    {
        if (instance == null)
            instance = this;

        currentRGB = 255;
        BackGround = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        Sun = GameObject.Find("Sun").GetComponent<SpriteRenderer>();
        SunSprite = Sun.GetComponent<SpriteRenderer>().sprite;
        TimersUp = true;

        Decorations = GameObject.Find("Decorations").transform;
        
    }

    private void FixedUpdate()
    {
        if (!Night)
        {
            if (TimersUp)
            {
                if (currentRGB > 40)
                {
                    BackGround.color = new Color32((byte)currentRGB, (byte)currentRGB, (byte)currentRGB, (byte)255);
                    
                    for (int i = 0; i < Decorations.childCount; i++)
                    {
                        Decorations.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)currentRGB, (byte)currentRGB, (byte)currentRGB, (byte)255);
                    }
                    currentRGB -= 0.5f;
                    StartCoroutine(WaitForSecond());
                    //Debug.Log("ColorChanged to " + Sky.color);
                }
                else
                    Night = true;
            }
        }

        if (Night)
        {
            if (TimersUp)
            {
                if (currentRGB < 255)
                {
                    BackGround.color = new Color32((byte)currentRGB, (byte)currentRGB, (byte)currentRGB, (byte)255);
                    
                    currentRGB += 0.5f;
                    StartCoroutine(WaitForSecond());
                    //Debug.Log("ColorChanged to " + Sky.color);
                }
                else
                    Night = false;

            }
        }


    }

    public IEnumerator WaitForSecond()
    {
        TimersUp = false;
        yield return new WaitForSeconds(0.1f);
        TimersUp = true;
        
    }

}
