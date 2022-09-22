using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private Vector2 AttackDamage = new Vector2(20f,30f);
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
    public bool isFighting = false;
    public bool isAttacking;
    public bool disableAttack;
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
            isAttacking = true;
        }
        anim.SetInteger("noOfClick", noOfClicks);

        anim.SetBool("isFighting", isFighting);





        Time.timeScale += (1f / slowdownLength) *Time.unscaledDeltaTime;
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
        playermovement.MoveSpeed = speedWhileAttacking;

    }
    public void FreeMovement()
    {
        disableAttack = false;

        isAttacking = false;
        playermovement.MoveSpeed = DefaultSpeed;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius,EnemyLayer);
        DisableMovement();

        foreach (Collider2D enemy in hitEnemies)
        {
            
            if (enemy.CompareTag("Barrel"))
            {
                enemy.GetComponent<BarrelHealth>().BarrelReceiveDamage(UnityEngine.Random.Range(AttackDamage.x,AttackDamage.y));

            }
            else if (enemy.CompareTag("enemy"))
            {
                var enemy_ = enemy.GetComponent<Enemy>();
                var enemy_health = enemy.GetComponent<enemyHealth>();
                enemy_health.EnemyReceiveDamage(UnityEngine.Random.Range(AttackDamage.x, AttackDamage.y));

            }

            //StartCoroutine(DoSlowMotion());
            StopCoroutine(StartedFighting());
            StartCoroutine(StartedFighting());

        }
    }
    public void ScreenShake()
    {
        src.GenerateImpulse();
    }
    public void Attack_byamount(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {

            if (enemy.CompareTag("Barrel"))
            {
                enemy.GetComponent<BarrelHealth>().BarrelReceiveDamage(damage);
                src.GenerateImpulse();

            }
            else if (enemy.CompareTag("enemy"))
            {
                var enemy_ = enemy.GetComponent<Enemy>();
                var enemy_health = enemy.GetComponent<enemyHealth>();
                enemy_health.EnemyReceiveDamage(damage);
                src.GenerateImpulse();

            }

            //StartCoroutine(DoSlowMotion());
            StopCoroutine(StartedFighting());
            StartCoroutine(StartedFighting());

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
