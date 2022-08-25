using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionDamage;
    [SerializeField] float explosionSpeed=100f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = explosionSpeed * Time.fixedDeltaTime * Vector2.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetComponent<Animator>().SetTrigger("Explosion");
            collision.GetComponent<enemyHealth>().EnemyReceiveDamage(explosionDamage);
        }
    }
}
