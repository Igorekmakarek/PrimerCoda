using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    bool InWater;

    private void Start()
    {
        InWater = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundAndMusic.instance.ChangeBackgroundSound(InWater);
        if (collision.CompareTag("Fish"))
            Destroy(collision.gameObject);

        if (collision.CompareTag("Bites"))
            Destroy(collision.gameObject);

        if (collision.CompareTag("Player"))
        {
            if (Player.instance.PlayerJumping)
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, -5f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
            if (!Player.instance.PlayerJumping)
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, 5f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.instance.PlayerJumping)
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, -1f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
            if (!Player.instance.PlayerJumping)
                Player.instance.PlayerRigidbody.AddForce(new Vector3(0f, 1f, 0f) * Player.instance.mass, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InWater = !InWater;
            Player.instance.PlayerJumping = !Player.instance.PlayerJumping;
            
        }
    }
}
