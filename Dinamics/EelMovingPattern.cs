using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMovingPattern : MonoBehaviour
{
    public float speed = 0.35f;

    Vector2 upperPos;
    Vector2 lowerPos;

    int randnum;

    bool DirectionChanged;

    bool moveUp;

    void Start()
    {
        randnum = Random.Range(3,6);
        
    }

    void Update()
    {
        upperPos = new Vector2(transform.position.x, transform.position.y + randnum);
        lowerPos = new Vector2(transform.position.x, transform.position.y - randnum);

        if (DirectionChanged)
        {

            if (moveUp)
            {
                moveTowards(upperPos);

            }
            else
            {
                moveTowards(lowerPos);
            }
        }
        else
            StartCoroutine(WaitForTime());

        
    }

    IEnumerator WaitForTime()
    {
        DirectionChanged = true;
        moveUp = !moveUp;
        yield return new WaitForSeconds(2f);
        DirectionChanged = false;
    }

    void moveTowards(Vector2 position)
    {
        transform.position = Vector2.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
