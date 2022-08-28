using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandsDown : MonoBehaviour
{
    PlayerAttack playerattack;
    [SerializeField]Transform attackPos;
    [SerializeField] float attackRadius;
    [SerializeField]LayerMask EnemyLayer;
    private CinemachineImpulseSource src;

    private void Awake()
    {
        playerattack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        src = GetComponent<CinemachineImpulseSource>();

        //attackPos.position = new Vector2(attackPos.position.x + 2.29f, attackPos.position.y + -22f);

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
    public void HandsDownAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            var enemy_ = enemy.GetComponent<Enemy>();
            var enemy_health = enemy.GetComponent<enemyHealth>();

            enemy_health.EnemyReceiveDamage(40);
            //StartCoroutine(DoSlowMotion());
            src.GenerateImpulse();
        }
    }
}
