using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusic : MonoBehaviour
{
    public static SoundAndMusic instance = null;

    public AudioClip DefaultMusic;
    public AudioClip BossMusic;
    public AudioClip CoinPickedUp;
    public AudioClip UnderWater;
    public AudioClip InSky;
    public AudioClip JumpOutOfWater;
    public AudioClip DropInWaterSound;

    private AudioSource Music;
    private AudioSource Background;


    void Start()
    {
        if (instance == null)
            instance = this;

        Music = transform.GetChild(3).GetComponent<AudioSource>();
        Background = transform.GetChild(4).GetComponent<AudioSource>();

    }

    public void ChangeBackgroundSound(bool InWater)
    {
//        Debug.Log(InWater);

        if (InWater)
        {
            Background.clip = UnderWater;
            DropInWater();
        }
        else
        {
            Background.clip = InSky;
            JumpOut();
        }
    }

     void DropInWater()
    {
        SoundManager.instance.PlaySingle(DropInWaterSound);
    }

     void JumpOut()
    {
        SoundManager.instance.PlaySingle(JumpOutOfWater);
    }

    public void PlayBossMusic()
    {
        Music.clip = BossMusic;
        //Music.volume = 0.6f;
        Music.Play();
    }

    public void PlayDefaultMusic()
    {
        Music.volume = 0.2f;
        Music.clip = DefaultMusic;
        Music.Play();
    }


    
}
