using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    public static Logo instance;

    [HideInInspector]
    public Image logo;

    public AudioClip clip;

    private bool StartFade;

    private int currentFade;

    void Start()
    {
        if (instance == null)
            instance = this;

        currentFade = 255;
        logo = GetComponent<Image>();
        logo.enabled = true;

        if (PlayerPrefs.GetInt("logo") == 0)
            StartCoroutine(logotime());
        else
            logo.enabled = false;
    }

    private IEnumerator logotime()
    {
        SoundManager.instance.PlaySingle(clip);
        SoundManager.instance.musicSourse.volume = 0;
        SoundManager.instance.backgroundSource.volume = 0;
        yield return new WaitForSeconds(2.5f);
        StartFade = true;
        

    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("logo") == 0)
        {
            if (StartFade)
            {
                if (SoundManager.instance.musicSourse.volume < 0.6f)
                {
                    SoundManager.instance.musicSourse.volume += 0.01f;
                    SoundManager.instance.backgroundSource.volume = 0.01f;
                }
                currentFade = currentFade - 3;
                logo.color = new Color32(255, 255, 255, (byte)currentFade);

                if (currentFade > 0 && currentFade < 5)
                    logo.enabled = false;

            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("logo", 0);
    }
}
