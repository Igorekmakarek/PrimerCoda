using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSituations : MonoBehaviour
{
    public static RandomSituations instance = null;

    public float CavePoints;
    bool inCave;

    int NextPoint;

    private void Start()
    {
        if (instance == null)
            instance = this;

        GenerateSituationsPoints();
        inCave = false;
        NextPoint = 0;
    }

    private void GenerateSituationsPoints()
    {
        CavePoints = Random.Range(40 + NextPoint, 80 + NextPoint);
        //Debug.Log("Cave generated at " + CavePoints);
    }

    //public void CheckForSituations(float Score)
    //{
    //    if (Score >= CavePoints)
    //    {
    //        if (!inCave)
    //        {
    //            //ChangeMap.instance.SwimInCave();
    //            inCave = true;
    //        }

    //        if (inCave) {
    //            if (Score >= CavePoints + 40f)
    //            {
    //                inCave = false;
    //                NextPoint += 100;
    //                GenerateSituationsPoints(); 
    //                ChangeMap.instance.SwimOutOfTheCave();

    //            }
    //        }

    //    }
    //}
}
