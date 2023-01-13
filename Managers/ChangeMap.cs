using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour
{
    public static ChangeMap instance = null;

    [HideInInspector]
    public GameObject Floor;
    [HideInInspector]
    public GameObject Ground;

    private Rigidbody2D FloorRigid;
    private Rigidbody2D GroundRigid;

    float Distance;

    [HideInInspector]
    public bool Cave = false;

    [HideInInspector]
    public bool FishSpooked = false;
    
    void Start()
    {
        if (instance == null)
            instance = this;

        Floor = gameObject.transform.GetChild(1).gameObject;
        Ground = Floor.transform.parent.GetChild(2).gameObject;

        FloorRigid = Floor.GetComponent<Rigidbody2D>();
        GroundRigid = Ground.GetComponent<Rigidbody2D>();
    }


    //public void MoveFloorUp()
    //{
    //    FloorRigid.AddForce(new Vector3(0f, 2f, 0f) * 100f, ForceMode2D.Force);
    //}

    //public void MoveFloorDown()
    //{
    //   FloorRigid.AddForce(new Vector3(0f, -2f, 0f) * 100f, ForceMode2D.Force);
    //}

    //public void MoveGroundUp()
    //{
    //    GroundRigid.AddForce(new Vector3(0f, 2f, 0f) * 100f, ForceMode2D.Force);
    //}
    //public void MoveGroundDown()
    //{
    //    GroundRigid.AddForce(new Vector3(0f, -2f, 0f) * 100f, ForceMode2D.Force);
    //}

    //public void SwimInCave()
    //{
    //    GameManager.instance.CanSpawn = false;
    //    Cave = true;
    //    MoveFloorDown();
    //    MoveGroundUp();
    //    ChangeSpawnTime();
    //    ScareAllFishes();
       
    //}

    //public void SwimOutOfTheCave()
    //{
    //    GameManager.instance.CanSpawn = true;
    //    Cave = false;
    //    FloorRigid.WakeUp();
    //    GroundRigid.WakeUp();
    //    MoveFloorUp();
    //    MoveGroundDown();
    //    ChangeSpawnTime();
    //    ScareAllFishes();
    //}

    //void ChangeSpawnTime()
    //{
    //    if (Cave)
    //        Spawner.instance.SetCaveTime();
    //    if (!Cave)
    //        Spawner.instance.SetTimeToDefault();
    //}


    public void ScareAllFishes()
    {
        for (int i = 0; i < GenerateFish.instance.FishParent.childCount; i++)
        {
            GenerateFish.instance.FishParent.GetChild(i).GetComponent<Fish>().SpookFish();
        }
    }

//    private void Update()
//    {
//        Distance = Vector3.Distance(Floor.transform.position, Ground.transform.position);
////        Debug.Log("Current distance is " + Distance);

//        if (Cave)
//        {
//            if (Distance < 25f + Player.instance.mass)
//            {
//                FloorRigid.Sleep();
//                GroundRigid.Sleep();
//                GameManager.instance.CanSpawn = true;
//            }
//        }

//        if (!Cave)
//        {
//            if (Distance > 75f + Player.instance.mass)
//            {
//                FloorRigid.Sleep();
//                GroundRigid.Sleep();
//                GameManager.instance.CanSpawn = true;
//            }
//        }
         
//    }

}
