using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private Vector2 AttackDamage = new Vector2(20f,30f);
    [SerializeField]private float AttackRadius = 0.2f;
    [SerializeField] Transform AttackPoint;
    private LayerMask EnemyLayer;
    Animator anim;


    Playermovement playermovement;
    float DefaultSpeed;
    [SerializeField] float speedWhileAttacking;
    Rigidbody2D rb;


    public int noOfClicks = 0;

    private CinemachineImpulseSource src;
    public bool isFighting = false;
    public bool isAttacking;
    public bool disableAttack;
    private float DownAttackForceCooldDown=0;
    [SerializeField]private float DownAttackForceCooldDownTimeMax = .2f;
    private bool canGetForceFromDownAttack=true;
    private float defualtGravityScale;

    void Awake()
    {
        DownAttackForceCooldDown = DownAttackForceCooldDownTimeMax;
        EnemyLayer = LayerMask.GetMask("Enemy");
        anim = GetComponent<Animator>();
        playermovement = GetComponent<Playermovement>();
        DefaultSpeed = playermovement.MoveSpeed;
        speedWhileAttacking = DefaultSpeed * speedWhileAttacking;
        src = GetComponent<CinemachineImpulseSource>();
        rb = GetComponent<Rigidbody2D>();
        defualtGravityScale = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S)&&!playermovement.IsGrounded )
        {
            anim.SetTrigger("DownAttack");
            isAttacking = true;

        }
        else if (Input.GetMouseButtonDown(0)&&noOfClicks<1 && !Input.GetKey(KeyCode.S))
        {
            noOfClicks=1;
            anim.SetBool("Attack1", true);
            isAttacking = true;
        }
        
        anim.SetInteger("noOfClick", noOfClicks);

        anim.SetBool("isFighting", isFighting);

        if (!canGetForceFromDownAttack)
        {
            DownAttackForceCooldDown -= Time.deltaTime;
            if (DownAttackForceCooldDown <= 0)
            {
                canGetForceFromDownAttack = true;
                DownAttackForceCooldDown = DownAttackForceCooldDownTimeMax;
            }
        }
       // Debug.Log(Time.timeScale);
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);



    }
    public IEnumerator StartedFighting()
    {
        isFighting = true;
        yield return new WaitForSeconds(12f);
        isFighting = false;
    }
    
    public void DisableMovement()
    {
        disableAttack = true;

        if(playermovement.IsGrounded)
            playermovement.MoveSpeed = speedWhileAttacking;
        else
        {
            
            Playermovement.isUsingVelocity = true;
            rb.velocity = Vector2.zero;
            Debug.Log("Happend");
            rb.gravityScale = 0f;
        }
    }
    public void FreeMovement()
    {
        disableAttack = false;
        Playermovement.isUsingVelocity = false;
        isAttacking = false;
        rb.gravityScale = defualtGravityScale;
        playermovement.MoveSpeed = DefaultSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);  
    }

}
