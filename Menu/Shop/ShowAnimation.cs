using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowAnimation : MonoBehaviour
{
    public Transform Destignation;
    private Vector3 DefPos;
    private Vector3 currentPos;

    [SerializeField]
    public bool ShowBool;
    [SerializeField]
    public bool HideBool;

    private void Start()
    {
        DefPos = transform.position;
    }

    public void Show()
    {
        transform.position = new Vector2(transform.position.x, Destignation.transform.position.y);
        transform.position = Vector3.Lerp(transform.position, Destignation.transform.position, 2f * Time.deltaTime);
        
    }

    public void Hide()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(Destignation.transform.position.x - 50f, transform.position.y, transform.position.z), 0.7f * Time.deltaTime);
          
    }

    public IEnumerator TimerToShow()
    {
        ShowBool = true;
        MenuScript.instance.ShopButton.enabled = false;
        yield return new WaitForSeconds(2f);
        MenuScript.instance.ShopButton.enabled = true;
        ShowBool = false;
    }

    public IEnumerator TimerToHide()
    {
        HideBool = true;
        MenuScript.instance.ShopButton.enabled = false;
        yield return new WaitForSeconds(1f);
        MenuScript.instance.ShopButton.enabled = true;
        HideBool = false;
        transform.position = DefPos;
    }

    private void FixedUpdate()
    {
        if (ShowBool)
           Show();

        if (HideBool)
           Hide();
    }

}

