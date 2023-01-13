using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Item item;

    private Image Icon;
    private Text Coins;
    private Image CoinImage;
    private Text Gems;
    private Image GemImage;
    private Button button;
    private Text YouHave;




    private void Start()
    {
        Icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        Coins = gameObject.transform.GetChild(1).GetComponent<Text>();
        CoinImage = Coins.transform.GetChild(0).GetComponent<Image>();
        Gems = gameObject.transform.GetChild(2).GetComponent<Text>();
        GemImage = Gems.transform.GetChild(0).GetComponent<Image>();
        YouHave = gameObject.transform.GetChild(3).GetComponent<Text>();

        //PlayerPrefs.SetInt("coins", 200); PlayerPrefs.SetInt("gems", 200);



        button = GetComponent<Button>();
        button.onClick.AddListener(OnSlotClicked);
       
    }

    public void HideComponents()
    {
        Icon.enabled = false;
        Coins.enabled = false;
        CoinImage.enabled = false;
        YouHave.enabled = false;
    }

    public void ShowComponents()
    {
        ReloadComponents();
        Icon.enabled = true;
        Coins.enabled = true;
        CoinImage.enabled = true;
        YouHave.enabled = true;
    }

    public void ReloadComponents()
    {
        Icon.sprite = item.icon;
        Coins.text = item.Coins + "";
        Gems.text = item.Gems + "";
        CheckLanguage();
    }

    public void CheckLanguage()
    {
        if (Language.instance.isRussian)
            YouHave.text = "У вас: " + PlayerPrefs.GetInt(item.PlayerPrefsName);
        else
            YouHave.text = "You have: " + PlayerPrefs.GetInt(item.PlayerPrefsName);
    }

    private void OnSlotClicked()
    {
        removeListeners();


        ShopScript.instance.dialogue.ShowDialogue();
        Info.instance.GetInfoAbout(item);

        ShopScript.instance.dialogue.No.onClick.AddListener(ShopScript.instance.dialogue.HideDialogue);
        ShopScript.instance.dialogue.Yes.onClick.AddListener(ShopScript.instance.dialogue.HideDialogue);
//        ShopScript.instance.dialogue.No.onClick.AddListener(Info.instance.HideComponents);
       // ShopScript.instance.dialogue.Yes.onClick.AddListener(Info.instance.HideComponents);
        ShopScript.instance.dialogue.Yes.onClick.AddListener(delegate { ShopScript.instance.BuyItem(item); });
        
    }

    private void removeListeners()
    {
        ShopScript.instance.dialogue.No.onClick.RemoveAllListeners();
        ShopScript.instance.dialogue.Yes.onClick.RemoveAllListeners();
    }


}
