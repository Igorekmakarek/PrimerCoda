using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;

    private int SoundOn;

    private Image image;
    private Button button;

    private void Start()
    {
        SoundOn = PlayerPrefs.GetInt("sound");
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Change);

        if (PlayerPrefs.GetInt("sound") == 0)
            SoundManager.instance.Mute();

        CheckSprite();
    }

    public void Change()
    {
        if (SoundOn == 1)
        {
            SoundOn = 0;
            SoundManager.instance.Mute();
        }
        else
        {
            SoundOn = 1;
            SoundManager.instance.UnMute();
        }

        CheckSprite();
        PlayerPrefs.SetInt("sound", SoundOn);
    }

    private void CheckSprite()
    {
        if (SoundOn == 1)
            image.sprite = On;
        else
            image.sprite = Off;
        
    }
}
