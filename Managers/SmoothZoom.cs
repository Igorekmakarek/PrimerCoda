using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothZoom : MonoBehaviour
{

    public static SmoothZoom instance = null;

    public float Speed;

    bool ZoomActive;
    float Size;

   
    


    void Start()
    {
        if (instance == null)
            instance = this;

       
    }

    private void LateUpdate()
    {
        if (ZoomActive)
        {
            GameManager.instance.MainCamera.orthographicSize = Mathf.Lerp(GameManager.instance.MainCamera.orthographicSize, Size, Speed);
                
        }

        
    }

    public void StartZooming(float size)
    {
        ZoomActive = true;
        Size = size;
    }

    //public void ChangeView()
    //{ 

    //    if (GameManager.instance.BossFight)
    //        MainTransform.position = Vector3.Lerp(MainTransform.position, new Vector3(MainTransform.position.x + 10f, MainTransform.position.y, MainTransform.position.z), 0.025f);
    //    else
    //        MainTransform.position = Vector3.Lerp(MainTransform.position, new Vector3(MainTransform.position.x - 10f, MainTransform.position.y, MainTransform.position.z), 0.025f);
    //}


    



}
