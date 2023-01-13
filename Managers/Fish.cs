using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    float mass;

    private void Start()
    {
        mass = gameObject.GetComponent<Rigidbody2D>().mass; 
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Rigidbody2D>() == null)
            return;

        float FishMass = collider.GetComponent<Rigidbody2D>().mass;

        if (collider.transform.CompareTag("Fish"))
        {
            //Чтобы поглотить, нужно быть на 30% больше
            if (FishMass + FishMass * 30 / 100 < mass)
            {
                mass += FishMass / 10;
                gameObject.transform.localScale = new Vector3(mass, mass, 1f);
                GetComponent<Animator>().SetTrigger("Eat");
                Destroy(collider.gameObject);
            }
            if (FishMass > mass + mass * 30 / 100)
            {
                Destroy(gameObject);
            }

            float SredMass = (mass + FishMass) / 2;

            if (Mathf.Abs(FishMass - mass) < SredMass / 100 * 30 + SredMass)
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 0f) * SredMass, ForceMode2D.Impulse);

        }

        if (collider.transform.CompareTag("Bites"))
        {
            Destroy(gameObject);
        }

    }

    public void SpookFish()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 0f) * mass, ForceMode2D.Force);
        gameObject.tag = "ScaredFish";
    }
}
