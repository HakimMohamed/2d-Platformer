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

        if(StartedSlowMotion)
            Time.timeScale += (1f / slowdownLength) *Time.unscaledDeltaTime;

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

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius,EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            if (enemy.CompareTag("Barrel"))
            {
                enemy.GetComponent<BarrelHealth>().BarrelReceiveDamage(100f);

            }
            else if (enemy.CompareTag("enemy"))
            {
                var enemy_ = enemy.GetComponent<Enemy>();
                var enemy_health = enemy.GetComponent<enemyHealth>();
                enemy_health.EnemyReceiveDamage(UnityEngine.Random.Range(AttackDamage.x, AttackDamage.y));
                ScreenShake();
                StartCoroutine(DoSlowMotion());

            }

            StopCoroutine(StartedFighting());
            StartCoroutine(StartedFighting());

        }
    }
    public void DownAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (canGetForceFromDownAttack)
            {
                Playermovement.isUsingVelocity = true;
                rb.velocity = new Vector2(0,20f);
                Debug.Log("Happend");
                

            }
            if (enemy.CompareTag("Barrel"))
            {
                enemy.GetComponent<BarrelHealth>().BarrelReceiveDamage(100f);

            }
            else if (enemy.CompareTag("enemy"))
            {
                var enemy_ = enemy.GetComponent<Enemy>();
                var enemy_health = enemy.GetComponent<enemyHealth>();
                enemy_health.EnemyReceiveDamage(UnityEngine.Random.Range(AttackDamage.x, AttackDamage.y));
                ScreenShake();
                StartCoroutine(DoSlowMotion());

            }
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
                ScreenShake();
                Instantiate(GameAssets.instance.ThunderHit, enemy.transform.position, Quaternion.identity);
                StartCoroutine(DoSlowMotion());

            }

            StopCoroutine(StartedFighting());
            StartCoroutine(StartedFighting());

        }
    }
    public float slowDownFactor = 0.05f;
    public float slowdownLength=2f;
    bool StartedSlowMotion = false;
    //public void StopSlowMotion()
    //{
    //    Time.timeScale = 1f;
    //}
    public IEnumerator DoSlowMotion()
    {
        StartedSlowMotion = true;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;
        StartedSlowMotion = false;
    }
    public void StartSlowMotion()
    {
        StartCoroutine(DoSlowMotion());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);  
    }

}
