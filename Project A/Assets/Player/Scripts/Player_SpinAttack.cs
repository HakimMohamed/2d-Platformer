using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpinAttack : MonoBehaviour
{
    Animator anim;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S)&&Time.time > nextAttackTime )
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Spin");
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
    }
}
