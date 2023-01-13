using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance = null;

    [HideInInspector]
    public Button StartButton;
    [HideInInspector]
    public Button ShopButton;
    [HideInInspector]
    public Button RecordsButton;

    public AudioClip BubblePop;

   

    private void Start()
    {

        if (instance == null)
            instance = this;


        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        ShopButton = StartButton.transform.parent.GetChild(1).GetComponent<Button>();
        RecordsButton = StartButton.transform.parent.GetChild(2).GetComponent<Button>();
        

        StartButton.onClick.AddListener(BlowTrigger);
        ShopButton.onClick.AddListener(ShopScript.instance.OpenOrClose);
        RecordsButton.onClick.AddListener(RecordsScript.instance.OpenOrClose);

    }

    public void BlowTrigger()
    {
        PopBubbleSound();
        StartButton.GetComponent<Animator>().SetTrigger("Clicked");
    }

    public void StartGame()
    {

        PlayerPrefs.SetString("Type", "SinglePlayer");
        Destroy(StartButton.gameObject);
       
        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
        
    }

    public void PopBubbleSound()
    {
        SoundManager.instance.PlaySingle(BubblePop);
    }

    

}
