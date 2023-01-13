using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public string DialogueString;
    private Text DialogueText;
    private Image DialogueFont;

    [SerializeField]
    public Button Yes;
    [SerializeField]
    public Button No;

    private void Start()
    {
        DialogueFont = GetComponent<Image>();
        DialogueText = DialogueFont.transform.GetChild(0).GetComponent<Text>();
        Yes = DialogueFont.transform.GetChild(1).GetComponent<Button>();
        No = DialogueFont.transform.GetChild(2).GetComponent<Button>();
        No.onClick.AddListener(HideDialogue);
        Yes.onClick.AddListener(HideDialogue);

        DialogueText.text = DialogueString;
    }

    public void ShowDialogue()
    {

        HideDialogue();
        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), .5f).setEaseOutBack();

        DialogueFont.enabled = true;
        DialogueText.enabled = true;
        Yes.GetComponent<Image>().enabled = true;
        No.GetComponent<Image>().enabled = true;

    }

    public void HideDialogue()
    {
        gameObject.LeanScale(Vector3.zero, .3f).setEaseInBack();
    }
}
