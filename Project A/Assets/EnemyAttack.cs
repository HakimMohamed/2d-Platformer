using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    private float AttackDamage = 40f;
    public float AttackRate = 2f;
    private float nextTimeAttack;
    [SerializeField] private float AttackRadius = 0.5f;
    [SerializeField] Transform AttackPoint;
    [SerializeField]private LayerMask playerLayer;
    Animator anim;
    Transform Player;

    Enemy enemy;


    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
        enemy = transform.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {           
            anim.SetTrigger("Attack1");
        }
        if (Time.time >= nextTimeAttack&& Vector2.Distance(transform.position, Player.position) < 1f)
        {
            anim.SetTrigger("Attack1");
            enemy.StartCoroutine(enemy.AttackCooldown());
            nextTimeAttack = Time.time+1f/AttackRate;
            
        }

    }

    public void Attack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, playerLayer);

        foreach (Collider2D Player in hitEnemies)
        {
            var player_health = Player.GetComponent<PlayerHealth>();

            player_health.PlayerReceiveDamage(AttackDamage);

        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }

}
