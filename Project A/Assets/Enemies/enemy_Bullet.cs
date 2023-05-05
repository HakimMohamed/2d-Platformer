using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Bullet : MonoBehaviour
{
    private float BulletSpeed=20f;
    GameObject Player;
    Rigidbody2D rb;
    [SerializeField] Vector2 bulletDamage = new(20, 35);
    void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = BulletSpeed * transform.right;

    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomBulletExplosion = Random.Range(0, 3);

        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PlayerHealth>().PlayerReceiveDamage(Random.Range(bulletDamage.x, bulletDamage.y));


            Bullet_Explosion(randomBulletExplosion);
            Destroy(gameObject);
            return;
        }
        Debug.Log(collision.name);
        Bullet_Explosion(randomBulletExplosion);
        Destroy(gameObject);
        
    }

    private void Bullet_Explosion(int whichExplosion)
    {
        if (whichExplosion == 1)
        {
            Instantiate(GameAssets.instance.enemy_bulletExplosion1, transform.position, Quaternion.identity);
        }
        else if (whichExplosion == 2)
        {
            Instantiate(GameAssets.instance.enemy_bulletExplosion2, transform.position, Quaternion.identity);

        }
    }
}
