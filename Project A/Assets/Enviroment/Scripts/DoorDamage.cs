using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamage : MonoBehaviour
{
    [Header("Attack")]
    private float AttackDamage = 40f;
    [SerializeField] private float AttackRadius = 0.5f;
    [SerializeField] Transform AttackPoint;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoorAttack()
    {
        float knockBackPower = 8f;

        Collider2D[] hit = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, playerLayer);

        foreach (Collider2D Player in hit)
        {
            var player_health = Player.GetComponent<PlayerHealth>();

            player_health.PlayerReceiveDamage(AttackDamage);
            StartCoroutine(Extensions.addForceToPlayer(Player.GetComponent<Rigidbody2D>(), (int)-transform.localScale.x, knockBackPower));


        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }
}
