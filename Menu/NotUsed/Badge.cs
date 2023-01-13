using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Badge : MonoBehaviour
{
    public string Title;
    public string Message;
    //[HideInInspector]
    public Image Icon;
    public Button Button;

    private void Start()
    {
        Icon = gameObject.GetComponent<Image>();
        Button = gameObject.GetComponent<Button>();
        Button.onClick.AddListener(delegate { BadgeInfo.instance.Badge(this); });
    }
}
