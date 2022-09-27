using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RangedAttack : MonoBehaviour
{


    [SerializeField] Transform attackPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x == 1)
        {
            attackPoint.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            attackPoint.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void Ranged_Attack()
    {
        Instantiate(GameAssets.instance.enemy_Bullet, attackPoint.position, attackPoint.rotation);
    }
}
