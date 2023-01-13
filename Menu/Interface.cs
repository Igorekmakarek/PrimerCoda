using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public static Interface instance = null;
    private Dialogue dialogue;

    private Image GameOverImage;
    private Text Title;
    private Text Score; //это в конце строка score
    private Text Coins;
    private Text NewBest;
    private Button ToMenuButton;
    private Text toMenuText;
    private Button RestartButton;
    private Text restartText;


    private Button HeartButton;
    private Text heartText;
    private Text howMuch;

    private bool HeartUsed;
    private bool AddShown;

    private Text ScoreText; //это score который видно при игре

    void Start()
    {
        if (instance == null)
            instance = this;

        

        GameOverImage = GameObject.Find("GameOver").GetComponent<Image>();
        Title = GameOverImage.transform.GetChild(0).GetComponent<Text>();
        Score = GameOverImage.transform.GetChild(1).GetComponent<Text>();
        Coins = GameOverImage.transform.GetChild(2).GetComponent<Text>();
        NewBest = GameOverImage.transform.GetChild(3).GetComponent<Text>();
        ToMenuButton = GameOverImage.transform.GetChild(4).GetComponent<Button>();
        toMenuText = ToMenuButton.transform.GetChild(0).GetComponent<Text>();
        RestartButton = GameOverImage.transform.GetChild(5).GetComponent<Button>();
        restartText = RestartButton.transform.GetChild(0).GetComponent<Text>();
        HeartButton = GameOverImage.transform.GetChild(6).GetComponent<Button>();
        heartText = HeartButton.transform.GetChild(0).GetComponent<Text>();
        howMuch = HeartButton.transform.GetChild(1).GetComponent<Text>();
        

        dialogue = GameOverImage.transform.GetChild(7).GetComponent<Dialogue>();


        if (Screen.width > 1100)
            GameOverImage.gameObject.transform.localScale = new Vector3(0.8f, 1f, 1f);

        ScoreText = GameObject.Find("Score").GetComponent<Text>();

        if (PlayerPrefs.GetString("Language") == "Rus")
            ScoreText.text = "Счет: ";
        else
            ScoreText.text = "Score: ";

    }

    void HideHeartButton()
    {
        if (HeartUsed)
        {
            HeartButton.GetComponent<Image>().enabled = false;
            heartText.enabled = false;
            howMuch.enabled = false;
        }
    }

    

    void ShowNewRecord()
    {
        if (GameManager.instance.score < PlayerPrefs.GetInt("HighScore"))
            NewBest.enabled = false;
    }

    void ChangeLanguage()
    {

        if (PlayerPrefs.GetString("Language") == "Rus")
        {
            Title.text = "Так держать!";
            ScoreText.text = "Очков: " + (int)GameManager.instance.score;
            Coins.text = "Монет: " + GameManager.instance.coins;
            NewBest.text = "Новый рекорд!";
            restartText.text = "Еще раз!";
            toMenuText.text = "В меню";
            heartText.text = "Вторая жизнь";

        }
        else
        {
            Title.text = "Well done!";
            ScoreText.text = "Score: " + (int)GameManager.instance.score;
            Coins.text = "Coins: " + GameManager.instance.coins;
            NewBest.text = "New Best!";
            restartText.text = "Play again";
            toMenuText.text = "Menu";
            heartText.text = "Second chance";
        }
    }


    

    //private void WatchVideoToReplay ()
    //{
    //    AdsCore.ShowAdsVideo("Rewarded_iOS");   //interstitital это с пропуском
    //    StartAgain();
    //}

    //void VideoClicked() //удвоение монет видео
    //{
    //    AdsCore.ShowAdsVideo("Rewarded_iOS");   
    //    GameManager.instance.coins *= 2;
    //}

    void StartAgain()
    {
        dialogue.HideDialogue();
        HideGameOver();
        StartCoroutine(GameManager.instance.Reincornation());
    }

    void HeartClicked()
    {
        HeartUsed = true;
        //dialogue.Yes.onClick.AddListener(WatchVideoToReplay);

        //Если игрок не собрал на этом кону сердец
        //и если у него вообще нет сердец
        //вывести окно о предложении просмотреть рекламу

        if (GameManager.instance.hearts == 0)
        {
            if (PlayerPrefs.GetInt("hearts") != 0)
            {
                PlayerPrefs.SetInt("hearts", PlayerPrefs.GetInt("hearts") - 1);
                StartAgain();
            }
            //else
            //    dialogue.ShowDialogue();
        }
        else
        {
            GameManager.instance.hearts--;
            StartAgain();
        }
    }


    public void ShowGameOver()
    {
        ChangeLanguage();
        HeartButton.GetComponent<Image>().enabled = true;
        HeartButton.enabled = true;
        heartText.enabled = true;
        howMuch.enabled = true;
 
        NewBest.enabled = true;

        HideHeartButton();
        ShowNewRecord();
        

        howMuch.text = PlayerPrefs.GetInt("hearts") + GameManager.instance.hearts + "";

        GameOverImage.enabled = true;
        Title.enabled = true;
        Score.text = ScoreText.text;
        Score.enabled = true;
        Coins.enabled = true;
        toMenuText.enabled = true;
        restartText.enabled = true;
        ToMenuButton.GetComponent<Image>().enabled = true;
        RestartButton.GetComponent<Image>().enabled = true;
        ToMenuButton.onClick.AddListener(GameManager.instance.ToMenu);
        RestartButton.onClick.AddListener(GameManager.instance.Restart);
        HeartButton.onClick.AddListener(HeartClicked);

    }

    private void HideGameOver()
    {
        NewBest.enabled = false;
        HeartButton.GetComponent<Image>().enabled = false;
        HeartButton.enabled = false;
        heartText.enabled = false;
        howMuch.enabled = false;


        GameOverImage.enabled = false;
        Title.enabled = false;
        Score.enabled = false;
        Coins.enabled = false;
        NewBest.enabled = false;
        toMenuText.enabled = false;
        restartText.enabled = false;
        ToMenuButton.GetComponent<Image>().enabled = false;
        RestartButton.GetComponent<Image>().enabled = false;
    }

    public void UpdateScore()
    {
        if (PlayerPrefs.GetString("Language") == "Rus")
            ScoreText.text = "Счет: " + (int)GameManager.instance.score;
        else
            ScoreText.text = "Score: " + (int)GameManager.instance.score;
    }

}
