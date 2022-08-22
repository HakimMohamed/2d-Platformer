using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("AttackSPeed")]
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    [Header("Attack Transistion")]
    int _currentAttack;

    bool _grounded;
    void Start()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
        anim = GetComponent<Animator>();
        playermovement = GetComponent<Playermovement>();
        DefaultSpeed = playermovement.MoveSpeed;
        speedWhileAttacking = DefaultSpeed * speedWhileAttacking;
    }

    // Update is called once per frame
    void Update()
    {
        _grounded = playermovement.IsGrounded;
        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) & _grounded )
            {
                DisableMovement();
                nextAttackTime = Time.time +2f / attackRate;
                // Reset timer

                _currentAttack=1;

                Debug.Log(Time.time);
                // Call one of the two attack animations "Attack1" or "Attack2"
                anim.SetTrigger("Attack" + _currentAttack);

                // Disable movement 
                

                _currentAttack++;
            }
        }
        else if(Time.time <= nextAttackTime&&_currentAttack==2)
        {
            if (Input.GetMouseButtonDown(0) & _grounded)
            {
                DisableMovement();

                nextAttackTime = Time.time + 1f / attackRate;

                _currentAttack = 2;


                Debug.Log(Time.time);
                // Call one of the two attack animations "Attack1" or "Attack2"
                anim.SetTrigger("Attack" + _currentAttack);

                // Disable movement 
                _currentAttack = 1;
            }
        }


        Time.timeScale += (1f / slowdownLength) *Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        Debug.Log(Time.timeScale);
    }
    public void DisableMovement()
    {
        playermovement.MoveSpeed = speedWhileAttacking;

    }
    public void FreeMovement()
    {
        playermovement.MoveSpeed = DefaultSpeed;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            var enemy_ = enemy.GetComponent<Enemy>();
            var enemy_health = enemy.GetComponent<enemyHealth>();

            enemy_health.EnemyReceiveDamage(AttackDamage);
            StartCoroutine(DoSlowMotion());
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
