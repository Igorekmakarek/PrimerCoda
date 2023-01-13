using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BadgeInfo : MonoBehaviour
{
    public static BadgeInfo instance = null;
    public Image BadgeBackground;

    public Image TitleBackground;
    public Text BadgeTitle;

    public Image MessageBackground;
    public Text BadgeMessage;

    public Image BadgeIcon;

    private bool Opened;

    private string[] badgeTitle;
    private string[] badgeMessage;

    void Start()
    {
        Opened = false;

        if (instance == null)
            instance = this;

        badgeMessage = new string[8];
        badgeTitle = new string[8];

        BadgeBackground = GameObject.Find("BadgeInfo").GetComponent<Image>();
        TitleBackground = BadgeBackground.transform.GetChild(0).GetComponent<Image>();
        BadgeTitle = TitleBackground.transform.GetChild(0).GetComponent<Text>();
        MessageBackground = BadgeBackground.transform.GetChild(1).GetComponent<Image>();
        BadgeMessage = MessageBackground.transform.GetChild(0).GetComponent<Text>();
        BadgeIcon = BadgeBackground.transform.GetChild(2).GetComponent<Image>();

        BedgeMessages();
    }

    public void Badge(Badge badge)
    {
        Debug.Log("Button pressed");
        BadgeTitle.text = badge.Title;
        BadgeMessage.text = badge.Message;
        BadgeIcon.sprite = badge.Icon.sprite;

        if (Opened)
            HideBadgeInfo();
        if (!Opened)
            ShowBadgeInfo();

        Opened = !Opened;

    }



    private void ShowBadgeInfo()
    {
        BadgeBackground.enabled = true;
        TitleBackground.enabled = true;
        BadgeTitle.enabled = true;
        MessageBackground.enabled = true;
        BadgeMessage.enabled = true;
        BadgeIcon.enabled = true;
    }

    private void HideBadgeInfo()
    {
        BadgeBackground.enabled = false;
        TitleBackground.enabled = false;
        BadgeTitle.enabled = false;
        MessageBackground.enabled = false;
        BadgeMessage.enabled = false;
        BadgeIcon.enabled = false;
    }


    private void BedgeMessages()
    {
        badgeTitle[0] = "Рекордсмен";
        badgeMessage[0] = "Набрано более 100 очков";

        badgeTitle[1] = "Обжора";
        badgeMessage[1] = "Съедено более 500 рыб за все время";

        badgeTitle[2] = "Трайхардер";
        badgeMessage[2] = "Сыграно более 100 попыток за все время";

        badgeTitle[3] = "Шопоголик";
        badgeMessage[3] = "Куплены все предметы в магазине";

        badgeTitle[4] = "Кощей";
        badgeMessage[4] = "Собрано более 100 монет за все время";

        badgeTitle[5] = "Братан!";
        badgeMessage[5] = "Встретиться с подводной братвой";

        badgeTitle[6] = "Холодильник";
        badgeMessage[6] = "Найти холодильник с Нюка-Колой";

        badgeTitle[7] = "Таинственный незнакомец";
        badgeMessage[7] = "Встретить таинственного незнакомца";

    }

}
