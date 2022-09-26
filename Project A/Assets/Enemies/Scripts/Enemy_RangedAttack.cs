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
        
    }

    public void Ranged_Attack()
    {
        Instantiate(GameAssets.instance.enemy_Bullet, attackPoint.position, Quaternion.identity);
    }
}
