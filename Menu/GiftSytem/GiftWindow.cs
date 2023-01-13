using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GiftWindow : MonoBehaviour
{
    private Animator animator;

    public GameObject ClaimYourAward;
    public Language lang;


    //=======ПОЛЯ ДЛЯ ОТСЧЕТА ВРЕМЕНИ==============
    public Text StartText;
    public Text StatusText;
    private bool canClaimReward;
    private int maxStreakCount = 10;
    private float claimCoolDown = 24f;
    private float claimDeadLine = 48f;

    //=======ПОЛЯ ДЛЯ ПОЛУЧЕНИЯ НАГРАДЫ===========
    public Gift[] Gifts;
    public Transform ButtonsParent;
    private Button[] Buttons = new Button[10];


    //========ПОЛЯ ДЛЯ ОКНА С ИНФОРМАЦИЕЙ===========
    public GameObject InfoWindow;
    public Transform ResourcesInfo;
    public Image[] ResoucesImages = new Image[8];

    public Sprite CoinSprite;
    public Sprite GemSprite;
    public Sprite HeartSprite;
    public Sprite MagnetSprite;
    public Sprite AcceleratorSprite;
    public Sprite SlowerSprite;
    public Sprite ProtectionSprite;
    public Sprite FishFoodSprite;

    private int numberOfResources;

    //=============ДЛЯ БЫСТРОГО ВЗАИМОДЕЙСТВИЯ(ПЕРЕВОДЫ И АВТОМАТИЧЕСКАЯ ЗАПИСЬ В PlayerPrefs)================
    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);

    }

    private DateTime? lastClaimTime         //знак вопроса добавляется что бы переменная могла быть null!
    {
        //get и set нужны что бы удобно сразу конвертировать в строку или в DateTime при получении/записи
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))        //если строка не null
                return DateTime.Parse(data);        //то мы меняем тип на DateTime и возвращаем 

            return null;
        }
        set
        {
            if (value != null)                  //если значение не null
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());     //сохраняем под ключем lastClaimedTime
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }

    private void Start()
    {
        ClaimYourAward.GetComponent<Text>().enabled = false;
        animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < ResourcesInfo.childCount; i++)
        {
            ResoucesImages[i] = ResourcesInfo.GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = ButtonsParent.GetChild(i).GetComponent<Button>();
            Buttons[i].onClick.AddListener(ClaimReward);
            Buttons[i].GetComponent<Image>().sprite = Gifts[i].Icon;
        }

        gameObject.GetComponent<Button>().onClick.AddListener(HideComponents);

        StartCoroutine(RewardsStateUpdater());

        StartCoroutine(RewardsWiggle());

        HideClaimed();
    }

    //=============================ПОКАЗ/СКРЫТИЕ КАРТЫ=================================
    public void ShowComponents()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), .3f).setEaseInOutBack();
    }

    private void HideComponents()
    {
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), .3f).setEaseInBack();
    }
    //================================================================================

    private IEnumerator RewardsWiggle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < Buttons.Length; i++)
                LeanTween.rotateZ(Buttons[i].gameObject, 10, 1f);

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < Buttons.Length; i++)
                LeanTween.rotateZ(Buttons[i].gameObject, -10, 1f);
        }
    }

    //==============================ОБНОВЛЕНИЕ ВРЕМЕНИ============================
    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRewardsState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;       //DateTime.UtcNow это время(сейчас) по гринвичу, которое во всех местах одинаковое(что бы не дюпали) ресы

            if (timeSpan.Hours > claimDeadLine)     //если прошло больше часов чем дедлайн, то обнуляем стрик 
            {
                lastClaimTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCoolDown)       //если прошло меньше времени чем наш кулдаун
                canClaimReward = false;
        }
        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        if (lang.isRussian)
            StartText.text = "Начало";
        else
            StartText.text = "Start";

        if (canClaimReward)
        {
            if (lang.isRussian)
                StatusText.text = "Получите вашу награду!";
            else
                StatusText.text = "Claim your reward!";
            
            ClaimYourAward.GetComponent<Text>().enabled = true;
        }
        else
        {
            var nextClaimTime = lastClaimTime.Value.AddHours(claimCoolDown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

            if (lang.isRussian)
                StatusText.text = $"Возвращайтесь через {cd}";
            else
                StatusText.text = $"Come back in {cd}";
        }

    }

    //============================ПРИ НАЖАТИИ НА НАГРАДУ=================================
    public void ClaimReward()
    {
        if (!canClaimReward)
            return;

        for (int i = 0; i < ResoucesImages.Length; i++)
        {
            ResoucesImages[i].enabled = false;
            ResoucesImages[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
        }

        Text text;

        if (Gifts[currentStreak].coins != 0)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + Gifts[currentStreak].coins);
            ResoucesImages[numberOfResources].sprite = CoinSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].coins;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].gems != 0)
        {
            PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") + Gifts[currentStreak].gems);
            ResoucesImages[numberOfResources].sprite = GemSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].gems;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].magnet != 0)
        {
            PlayerPrefs.SetInt("magnet", PlayerPrefs.GetInt("magnet") + Gifts[currentStreak].magnet);
            ResoucesImages[numberOfResources].sprite = MagnetSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].magnet;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].accelerator != 0)
        {
            PlayerPrefs.SetInt("accelerator", PlayerPrefs.GetInt("accelerator") + Gifts[currentStreak].accelerator);
            ResoucesImages[numberOfResources].sprite = AcceleratorSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].accelerator;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].slower != 0)
        {
            PlayerPrefs.SetInt("slower", PlayerPrefs.GetInt("slower") + Gifts[currentStreak].slower);
            ResoucesImages[numberOfResources].sprite = SlowerSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].slower;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].fishfood != 0)
        {
            PlayerPrefs.SetInt("fishfood", PlayerPrefs.GetInt("fishfood") + Gifts[currentStreak].fishfood);
            ResoucesImages[numberOfResources].sprite = FishFoodSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].fishfood;
            text.enabled = true;
            numberOfResources++;
        }

        if (Gifts[currentStreak].protection != 0)
        {
            PlayerPrefs.SetInt("protection", PlayerPrefs.GetInt("protection") + Gifts[currentStreak].protection);
            ResoucesImages[numberOfResources].sprite = ProtectionSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].protection;
            text.enabled = true;
            numberOfResources++;
        }
        if (Gifts[currentStreak].heart != 0)
        {
            PlayerPrefs.SetInt("hearts", PlayerPrefs.GetInt("hearts") + Gifts[currentStreak].heart);
            ResoucesImages[numberOfResources].sprite = HeartSprite;
            ResoucesImages[numberOfResources].enabled = true;
            text = ResoucesImages[numberOfResources].transform.GetChild(0).GetComponent<Text>();
            text.text = "" + Gifts[currentStreak].heart;
            text.enabled = true;
            numberOfResources++;
        }

        numberOfResources = 0;

        lastClaimTime = DateTime.UtcNow;

        Buttons[currentStreak].enabled = false;
        Buttons[currentStreak].gameObject.GetComponent<Image>().color = Color.grey;
        Buttons[currentStreak].gameObject.LeanScale(Vector3.zero, .3f).setEaseInOutCirc();

        StartCoroutine(ShowInfo());


        currentStreak = (currentStreak + 1) % maxStreakCount;

        animator.SetInteger("currentStreak", currentStreak + 1);


        if (currentStreak == 0)
            ShowAllRewards();

        UpdateRewardsState();
        ShopScript.instance.ChangeResouces();

        ClaimYourAward.GetComponent<Text>().enabled = false;

    }

    //=================ПОКАЗ/СКРЫТИЕ ИНФОРМАЦИИ О ПОЛУЧЕННОЙ НАГРАДЕ==================
    private IEnumerator ShowInfo()
    {
        yield return new WaitForSeconds(0.7f);
        LeanTween.scale(InfoWindow, new Vector3(1f, 1f, 1f), .3f).setEaseInOutBack();
        gameObject.GetComponent<Button>().enabled = false;
    }

    public void HideInfo()
    {
        LeanTween.scale(InfoWindow, new Vector3(0f, 0f, 0f), .3f).setEaseInBack();
        gameObject.GetComponent<Button>().enabled = true;
    }

    //=======================ПОКАЗ НАГРАД / СКРЫТИЕ НАГРАД============================

    private void ShowAllRewards()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            LeanTween.scale(Buttons[i].gameObject, new Vector3(1f, 1f, 1f), .3f).setEaseInCirc();
            Buttons[i].enabled = true;
            Buttons[i].gameObject.GetComponent<Image>().color = Color.white;

        }
    }

    private void HideClaimed()
    {
        for (int i = 0; i < currentStreak; i++)
        {
            Buttons[i].gameObject.LeanScale(Vector3.zero, .3f).setEaseInOutCirc();
        }

        animator.SetInteger("currentStreak", currentStreak + 1);

    }
    //================================================================================


}
