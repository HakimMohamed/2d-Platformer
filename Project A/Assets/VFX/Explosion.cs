using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionDamage;
    [SerializeField] float explosionSpeed=100f;
    Rigidbody2D rb;
    float dir;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = GameObject.Find("Player").transform.localScale.x;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = explosionSpeed * Time.fixedDeltaTime * Vector2.right*dir;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            GetComponent<Animator>().SetTrigger("Explosion");
            collision.GetComponent<enemyHealth>().EnemyReceiveDamage(explosionDamage);
        }
    }
}
