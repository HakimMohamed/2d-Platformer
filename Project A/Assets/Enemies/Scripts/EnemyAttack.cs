using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    private float AttackDamage = 40f;
    [SerializeField] private float AttackRadius = 0.5f;
    [SerializeField] Transform AttackPoint;
    [SerializeField]private LayerMask playerLayer;
   



   
    // Update is called once per frame
    void Update()
    {
        
       

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
