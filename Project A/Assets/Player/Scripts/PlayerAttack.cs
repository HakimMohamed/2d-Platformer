using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private float AttackDamage = 40f;
    [SerializeField]private float AttackRadius = 0.5f;
    [SerializeField] Transform AttackPoint;
    private LayerMask EnemyLayer;
    Animator anim;


    Playermovement playermovement;
    float DefaultSpeed;
    [SerializeField] float speedWhileAttacking;
    float attackCoolDownReset;



    public int noOfClicks = 0;
    private float timeBetweenAttacks = 1f;

    private CinemachineImpulseSource src;
    
    void Start()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
        anim = GetComponent<Animator>();
        playermovement = GetComponent<Playermovement>();
        DefaultSpeed = playermovement.MoveSpeed;
        speedWhileAttacking = DefaultSpeed * speedWhileAttacking;
        src = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&noOfClicks<1)
        {
            noOfClicks=1;
            anim.SetBool("Attack1", true);
            DisableMovement();
        }
        anim.SetInteger("noOfClick", noOfClicks);

        





        Time.timeScale += (1f / slowdownLength) *Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }

    
    public void DisableMovement()
    {
        
        playermovement.MoveSpeed = speedWhileAttacking;

    }
    public void FreeMovement()
    {
        playermovement.MoveSpeed = DefaultSpeed;
    }

    public void Attack(int attackDamage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            var enemy_ = enemy.GetComponent<Enemy>();
            var enemy_health = enemy.GetComponent<enemyHealth>();

            enemy_health.EnemyReceiveDamage(attackDamage);
            //StartCoroutine(DoSlowMotion());
            src.GenerateImpulse();
        }
    }
    
    public float slowDownFactor = 0.05f;
    public float slowdownLength=2f;

    //public void StopSlowMotion()
    //{
    //    Time.timeScale = 1f;
    //}
    public IEnumerator DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);  
    }

}
