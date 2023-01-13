using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static ShopScript instance = null;

    public AudioClip Bought;

    [SerializeField]
    public Dialogue dialogue;

    [SerializeField]
    public Monologue NotEnough;


    public Image BackGround;
    public Image[] SlotImage;
    public ShopSlot[] SlotScript;

    private int NumberOfSlots;

    private int coins;
    private int gems;
    private int hearts;
    private Text CoinsText;
    private Text GemsText;
    private Text HeartsText;

    bool Opened;

    private void Start()
    {
        if (instance == null)
            instance = this;

       


        dialogue = transform.GetChild(6).GetComponent<Dialogue>();
        NotEnough = transform.GetChild(7).GetComponent<Monologue>();

        CoinsText = GameObject.Find("CoinsMenu").transform.GetChild(0).GetChild(0).GetComponent<Text>();
        GemsText = GameObject.Find("GemsMenu").transform.GetChild(0).GetChild(0).GetComponent<Text>();
        HeartsText = GameObject.Find("HeartsMenu").transform.GetChild(0).GetChild(0).GetComponent<Text>();
        ChangeResouces();

        BackGround = GetComponent<Image>();

        GetSlotImages();
    }


    private void GetSlotImages()
    {
        NumberOfSlots = BackGround.transform.childCount-2;

        SlotImage = new Image[NumberOfSlots];

        for (int i = 0; i < NumberOfSlots; i++)
        {
            SlotImage[i] = BackGround.transform.GetChild(i).GetComponent<Image>();
            SlotScript[i] = SlotImage[i].GetComponent<ShopSlot>();
            SlotImage[i].sprite = SlotScript[i].item.icon;
        }

    }


    private void OpenShop()
    {
        Opened = true;

        for (int i = 0; i < NumberOfSlots; i++)
        {
            SlotImage[i].enabled = true;
            SlotScript[i].ShowComponents();
        }

        MenuScript.instance.PopBubbleSound();
        MenuScript.instance.ShopButton.GetComponent<Animator>().SetTrigger("Clicked");
        
    }

    private void CloseShop()
    {   
        Opened = false;

        MenuScript.instance.PopBubbleSound();
        MenuScript.instance.ShopButton.GetComponent<Animator>().SetTrigger("Clicked");

    }

    public void GetCoins(int number)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + number);
        ChangeCoins();
    }

    public void GetGems(int number)
    {
        PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") + number);
        ChangeGems();
    }

    public void GetHearts(int number)
    {
        PlayerPrefs.SetInt("hearts", PlayerPrefs.GetInt("hearts") + number);
        ChangeHearts();
    }

    public void ChangeResouces()
    {
        ChangeCoins();
        ChangeGems();
        ChangeHearts();
    }


    private void ChangeCoins()
    {
       
        coins = PlayerPrefs.GetInt("coins");
        CoinsText.text = "" + coins;
    }

    private void ChangeGems()
    {

        gems = PlayerPrefs.GetInt("gems");
        GemsText.text = "" + gems;
    }

    private void ChangeHearts()
    {

        hearts = PlayerPrefs.GetInt("hearts");
        HeartsText.text = "" + hearts;
    }

    public void OpenOrClose()
    {
        if (Opened)
            CloseShop();
        else
            OpenShop();
    }

    private ShopSlot SearchForItem(Item item)
    {
        for (int i = 0; i < NumberOfSlots; i++)
            if (SlotScript[i].item == item)
                return SlotScript[i];

        return null;
    }

    public void BuyItem(Item item)
    {
        if (item.Coins > PlayerPrefs.GetInt("coins") || item.Gems > PlayerPrefs.GetInt("gems"))
        {
            NotEnough.Show();
            return;
        }
//        GP.instance.GetAchievement(GP.itembought);
        SoundManager.instance.PlaySingle(Bought);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - item.Coins);
        PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") - item.Gems);

        PlayerPrefs.SetInt(item.PlayerPrefsName, PlayerPrefs.GetInt(item.PlayerPrefsName) + 1);

        SearchForItem(item).ReloadComponents();

        ChangeResouces();
    }
}
