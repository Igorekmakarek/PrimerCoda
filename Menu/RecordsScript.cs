using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecordsScript : MonoBehaviour
{
    public static RecordsScript instance = null;

    private Image BackGround;
    private Transform BadgesTransform;

    private Image[] TextFonts;
    public Image[] BadgesImage;

    private Image BadgeInfo;

    private Text BestScore;
    private Text MoneyCollected;
    private Text GamesPlayed;
    private Text FishesEated;
    private Text StuffBuyed;

    private Image ExitImage;
    private Button ExitButton;

    private bool opened;


    private void Awake()
    {
        
    }
    void Start()
    {
        if (instance == null)
            instance = this;

        BackGround = GameObject.Find("Records").GetComponent<Image>();

        TextFonts = new Image[5];
        BadgesImage = new Image[8];
        
       

        for (int i = 0; i < TextFonts.Length; i++)
        {
            TextFonts[i] = BackGround.transform.GetChild(i).GetComponent<Image>();
        }
        
        BestScore = TextFonts[0].transform.GetChild(0).GetComponent<Text>();
        MoneyCollected = TextFonts[1].transform.GetChild(0).GetComponent<Text>();
        GamesPlayed = TextFonts[2].transform.GetChild(0).GetComponent<Text>();
        FishesEated = TextFonts[3].transform.GetChild(0).GetComponent<Text>();
        StuffBuyed = TextFonts[4].transform.GetChild(0).GetComponent<Text>();

        BadgesTransform = BackGround.transform.GetChild(5).transform;
        BadgeInfo = BackGround.transform.GetChild(6).GetComponent<Image>();

        ExitImage = BackGround.transform.GetChild(7).GetComponent<Image>();
        ExitButton = ExitImage.GetComponent<Button>();

        ExitButton.onClick.AddListener(CloseAchievements);
        


        for (int i = 0; i < BadgesTransform.childCount; i++)
            BadgesImage[i] = BadgesTransform.GetChild(i).GetComponent<Image>();



        //ChangeLanguage();



    }

    public void ChangeLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "Rus")
        {
            BestScore.text = "Лучший счет: " + PlayerPrefs.GetInt("HighScore");
            MoneyCollected.text = "Монет собрано: " + PlayerPrefs.GetInt("AllTimeCoins");
            GamesPlayed.text = "Игр сыграно: " + PlayerPrefs.GetInt("NumberOfGames");
            FishesEated.text = "Рыб съедено: " + PlayerPrefs.GetInt("fishes");
            StuffBuyed.text = "Вещей куплено: " + PlayerPrefs.GetInt("StuffByed") + " / 32";
        }
        else
        {
            BestScore.text = "Best score: " + PlayerPrefs.GetInt("HighScore");
            MoneyCollected.text = "Money collected: " + PlayerPrefs.GetInt("AllTimeCoins");
            GamesPlayed.text = "Games played: " + PlayerPrefs.GetInt("NumberOfGames");
            FishesEated.text = "Fishes eaten: " + PlayerPrefs.GetInt("fishes");
            StuffBuyed.text = "Stuff Byed: " + PlayerPrefs.GetInt("StuffByed") + " / 32";
        }
    }

    public void OpenOrClose()
    {
        if (opened)
            CloseAchievements();
        else
            OpenAchievements();
    }

    private void OpenAchievements()
    {
        MenuScript.instance.PopBubbleSound();
        MenuScript.instance.RecordsButton.GetComponent<Animator>().SetTrigger("Clicked");

        BackGround.enabled = true;

        for (int i = 0; i < TextFonts.Length; i++)
            TextFonts[i].enabled = false;

        for (int i = 0; i < BadgesImage.Length; i++)
            BadgesImage[i].enabled = true;

        BestScore.enabled = true;
        MoneyCollected.enabled = true;
        GamesPlayed.enabled = true;
        FishesEated.enabled = true;
        StuffBuyed.enabled = true;

        ExitImage.enabled = true;
        ExitButton.enabled = true;

        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), .3f).setEaseInCirc();
        opened = true;

    }

    private void CloseAchievements()
    {

        gameObject.LeanScale(Vector3.zero, .3f).setEaseInOutCirc();
        opened = false;
    }

    private void SomeBadgePressed()
    {
        // for (int i = 0; i < )
    }

    


}
