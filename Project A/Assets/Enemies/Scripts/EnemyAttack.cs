using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField]private Vector2 AttackDamage = new(20f,35f);
    [SerializeField] private float AttackRadius = 0.5f;
    [SerializeField] Transform AttackPoint;

    [SerializeField]private LayerMask playerLayer;
 



    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position));
       

    }
    public void Attack()
    {
        
        Collider2D[] hitEnemies  = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, playerLayer);
       

        foreach (Collider2D Player in hitEnemies)
        {
            var player_health = Player.GetComponent<PlayerHealth>();

            player_health.PlayerReceiveDamage(Random.Range(AttackDamage.x,AttackDamage.y));

        }
       
    }
    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);     
    }

}
