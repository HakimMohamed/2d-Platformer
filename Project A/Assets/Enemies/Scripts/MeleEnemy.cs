using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float attackcooldown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D Boxcollider;
    [SerializeField] private LayerMask PlayerLayer;
    
    

    private void Update()
    {
        cooldownTimer += Time.time;
        //Attack only when player insgiht
        if (PlayerInSight())
        {


            if (cooldownTimer >= attackcooldown)
            {

                cooldownTimer = 0;

            }

        }

    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(Boxcollider.bounds.center+ transform.right * range * transform.localScale.x* colliderDistance, new Vector3(Boxcollider.bounds.size.x*range,Boxcollider.bounds.size.y,Boxcollider.bounds.size.z), 0, Vector2.left, 0, PlayerLayer);
        
        return hit.collider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Boxcollider.bounds.center + transform.right * range*transform.localScale.x * colliderDistance, Boxcollider.bounds.size);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            //DamagePlayer
        }
    }
}

