using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunEffect : MonoBehaviour
{
    ParticleSystem SunParticle;

    private void Start()
    {
        SunParticle = gameObject.GetComponent<ParticleSystem>();
    }


    private void Update()
    {
       
        if (SunParticle.time >= 5)
        {

            SunParticle.Clear();
            SunParticle.Play();
        }

    }
}
