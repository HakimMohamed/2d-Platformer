using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]string Name = " ";
    Transform Player;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField]private Image Enemy_Health_Bar;
    public static int enemies_Number;
    void Awake()
    {
        enemies_Number++;
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rb.velocity.normalized.x;
        anim.SetFloat("Speed", Mathf.Abs(speed));
        if (PlayerHealth.IsDead&&Name=="normalenemy")
        {
            GetComponent<EnemyAttack>().enabled=false;
        }
        else if (PlayerHealth.IsDead && Name == "hellbot")
        {
            GetComponent<EnemyHellBotAttack>().enabled = false;
            

        }
    }
    public void LookAtPlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            Flip(1);
        }
        else if (transform.position.x > Player.position.x)
        {
            Flip(-1);
        }


    }
    void Flip(int dir)
    {
        Vector3 scaler = transform.localScale;
        scaler.x = dir;
        transform.localScale = scaler;
        Flip_Ui();
    }
    void Flip_Ui()
    {
        Enemy_Health_Bar.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }
}
