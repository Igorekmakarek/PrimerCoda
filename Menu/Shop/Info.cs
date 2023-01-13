using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public static Info instance;


    private Image icon;

    public Text BuyItem;

    private Text Description;

    private Text YouHave;
    private Text Coins;
    private Image CoinImage;
    private Text Gems;
    private Image GemImage;

    private Text Cost;
    private Text CostCoins;
    private Image CostCoinImage;
    private Text CostGems;
    private Image CostGemImage;
    
    void Start()
    {
        if (instance == null)
            instance = this;

        icon = transform.GetChild(0).GetComponent<Image>();
        Description = transform.GetChild(1).GetComponent<Text>();
        YouHave = transform.GetChild(2).GetComponent<Text>();
        Coins = YouHave.transform.GetChild(0).GetComponent<Text>();
        CoinImage = YouHave.transform.GetChild(1).GetComponent<Image>();
        Gems = YouHave.transform.GetChild(2).GetComponent<Text>();
        GemImage = YouHave.transform.GetChild(3).GetComponent<Image>();

        Cost = transform.GetChild(3).GetComponent<Text>();
        CostCoins = Cost.transform.GetChild(0).GetComponent<Text>();
        CostCoinImage = Cost.transform.GetChild(1).GetComponent<Image>();
        CostGems = Cost.transform.GetChild(2).GetComponent<Text>();
        CostGemImage = Cost.transform.GetChild(3).GetComponent<Image>();

    }

    public void ChangeLanguage()
    {
        if (Language.instance.isRussian)
        {
            BuyItem.text = "Купить предмет?";
            YouHave.text = "У вас:";
            Cost.text = "Цена: ";
        }
        else
        {
            BuyItem.text = "Buy item?";
            YouHave.text = "Yours:";
            Cost.text = "Cost: ";
        }
            
    }

    private void ShowComponents(int GemsCost, int CoinCost)     //передаем сюда стоимость что бы знать, показывать ли гемы или монеты
    {                                                                           //(вдруг предмет можно будет купить только за гемы)
               
        
        icon.enabled = true;
        Description.enabled = true;

        YouHave.enabled = true;
        Cost.enabled = true;

        if (CoinCost != 0)
        {
            Coins.enabled = true;
            CoinImage.enabled = true;
            CostCoins.enabled = true;
            CostCoinImage.enabled = true;
        }
        

        if (GemsCost != 0)
        {
            Gems.enabled = true;
            GemImage.enabled = true;

            CostGems.enabled = true;
            CostGemImage.enabled = true;
        }
    }

    //public void HideComponents()
    //{
    //    icon.enabled = false;
    //    Description.enabled = false;

    //    YouHave.enabled = false;
    //    Cost.enabled = false;

    //    Coins.enabled = false;
    //    CoinImage.enabled = false;
    //    CostCoins.enabled = false;
    //    CostCoinImage.enabled = false;

    //    Gems.enabled = false;
    //    GemImage.enabled = false;
    //    CostGems.enabled = false;
    //    CostGemImage.enabled = false;
        
    //}



    public void GetInfoAbout(Item item)
    {
        if (Language.instance.isRussian)
            Description.text = item.Description;
        else
            Description.text = item.EngDescription;

        icon.sprite = item.icon;
        Coins.text = PlayerPrefs.GetInt("coins").ToString();
        Gems.text = PlayerPrefs.GetInt("gems").ToString();
        CostCoins.text = item.Coins.ToString();
        CostGems.text = item.Gems.ToString();

        ShowComponents(item.Gems, item.Coins);

    }



}
