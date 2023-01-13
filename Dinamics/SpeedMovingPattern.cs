using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMovingPattern : MonoBehaviour
{
    public float speed;

    bool canSpeedUp;

    private void Start()
    {
        if (speed == 0)
        speed = 0.5f;
    }

    private void Update()
    {
        if (canSpeedUp)
            SpeedUp();
        else
            StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(2f);
        canSpeedUp = !canSpeedUp;
        
    }

    void SpeedUp()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 5, transform.position.y), speed * Time.deltaTime);
    }
}
