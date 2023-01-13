
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monologue : MonoBehaviour
{
    public string MonologueString;
    public string EngMonologueString;
    private Image MonologueFont;
    private Text MonologueText;

 

    private void Start()
    {
        MonologueFont = GetComponent<Image>();
        MonologueText = MonologueFont.transform.GetChild(0).GetComponent<Text>();

        

        MonologueFont.GetComponent<Button>().onClick.AddListener(Hide);
    }

    public void Show()
    {

        Hide();

        if (Language.instance.isRussian)
            MonologueText.text = MonologueString;
        else
            MonologueText.text = EngMonologueString;

        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), .5f).setEaseOutBack();
    }

    public void Hide()
    {
        gameObject.LeanScale(Vector3.zero, .3f).setEaseInBack();
    }
}
