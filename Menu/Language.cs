using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public static Language instance;
    public Sprite Eng;
    public Sprite Rus;

    Image img;

    [HideInInspector]
    public bool isRussian;
    
    void Start()
    {
        instance = this;

        img = GetComponent<Image>();

        RememberLanguage();
        ChangeLanguage();

        GetComponent<Button>().onClick.AddListener(ChangeLanguage);
    }

    void RememberLanguage()
    {
        

        if (PlayerPrefs.GetString("Language") == "Rus")
            isRussian = false;
        else
            isRussian = true;
    }

    void ChangeLanguage()
    {
        

        if (isRussian)
        {
            PlayerPrefs.SetString("Language", "Eng");
            img.sprite = Eng;
            isRussian = false;
        }
        else
        {
            PlayerPrefs.SetString("Language", "Rus");
            img.sprite = Rus;
            isRussian = true;
        }

        

        RecordsScript.instance.ChangeLanguage();
        Info.instance.ChangeLanguage();

        for (int i = 0; i < ShopScript.instance.SlotScript.Length; i++)
            ShopScript.instance.SlotScript[i].CheckLanguage();

        

        Debug.Log(PlayerPrefs.GetString("Language"));
    }
}
