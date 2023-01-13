using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHide : MonoBehaviour
{
    bool Opened;

    ShowAnimation show;

    private void Start()
    {
        show = GetComponent<ShowAnimation>();
    }

    private void Show()
    {
        StartCoroutine(show.TimerToShow());
        Opened = true;
    }

    private void Hide()
    {
        StartCoroutine(show.TimerToHide());
        Opened = false;
    }

    public void ShowOrHideShop()
    {
        if (Opened)
            Hide();
        else
            Show();
    }
}
