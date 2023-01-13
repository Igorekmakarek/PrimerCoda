using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAndFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
            Destroy(collision.gameObject);

        if (collision.CompareTag("Bites"))
            Destroy(collision.gameObject);

        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "Floor")
            Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, -5f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
            if (gameObject.name == "Ground")
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, 5f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "Floor")
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, -3f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
            if (gameObject.name == "Ground")
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, 3f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
        }
    }

     
}
