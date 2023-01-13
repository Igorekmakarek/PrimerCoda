using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizingPattern : MonoBehaviour
{
    public float speed = 0.35f;

    Vector2 puffedScale;
    Vector2 defaultScale;

    bool ScaleChanged;

    bool moveUp;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    void Update()
    {
        puffedScale = new Vector2(transform.localScale.x + 5, transform.localScale.y + 5);

        if (ScaleChanged)
        {

            if (moveUp)
            {
                changeScale(puffedScale);

            }
            else
            {
                changeScale(defaultScale);
            }
        }
        else
            StartCoroutine(WaitForTime());


    }

    IEnumerator WaitForTime()
    {
        ScaleChanged = true;
        moveUp = !moveUp;
        yield return new WaitForSeconds(1.5f);
        ScaleChanged = false;
    }

    void changeScale(Vector2 scale)
    {
        transform.localScale = Vector2.Lerp(transform.localScale, scale, speed * Time.deltaTime);
    }
}
