using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().PlayerReceiveDamage(20f);
            Playermovement.isUsingVelocity = true;
            collision.transform.GetComponent <Rigidbody2D>().velocity = new Vector2(0, 20f);

        }
    }
}
