using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Bullet : MonoBehaviour
{
    private float BulletSpeed=20f;
    GameObject Player;

    [SerializeField] Vector2 bulletDamage = new(20, 35);
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += BulletSpeed * Time.deltaTime * Vector3.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PlayerHealth>().PlayerReceiveDamage(Random.Range(bulletDamage.x, bulletDamage.y));
            int randomBulletExplosion = Random.Range(0, 3);


            Bullet_Explosion(randomBulletExplosion);
            Destroy(gameObject);

        }
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
