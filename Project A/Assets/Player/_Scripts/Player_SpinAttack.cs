using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_SpinAttack : MonoBehaviour
{
    Animator anim;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;
    PlayerHealth playerhealth;
    private float manaUsage=10f;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerhealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)&&Time.time > nextAttackTime )
        {
            if (playerhealth.PlayerUseMana(manaUsage))
            {
                anim.SetTrigger("Spin");
                nextAttackTime = Time.time + 2f / attackRate;
            }
        }
    }
}
