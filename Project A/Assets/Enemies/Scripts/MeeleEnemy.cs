using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MeeleEnemy : MonoBehaviour
{
    [HideInInspector] public event EventHandler OnEnemyDamaged;

    [Header("Properties")]
    [HideInInspector]public float Health;
    [SerializeField] float MaxHealth;
    [SerializeField] Vector2 FollowRange;
    [SerializeField] float Speed;
    [SerializeField]private float GroundRadius = 0.5f;


    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBar_Stroke;

    [Header("Components")]
    [SerializeField]private LayerMask TargetLayer;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform GroundTransform;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;
    private Transform Target;

    [Header("States")]
    private bool IsDead = false;
    private bool IsTargetInRange;
    private bool IsGrounded;

    public int Angle;
    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.Find("Player").transform;


        //Set Values
        Health = MaxHealth;
        IsTargetInRange = false;
    }

    private void Update()
    {

        if (IsDead)
        {
            EnemyDissolve();
            return;
        }

        IsGrounded = Physics2D.OverlapCircle(GroundTransform.position, GroundRadius, GroundLayer);

        

        

    }
   
    //Follow the player
    


    public void EnemyReceiveDamage(float Damage)
    {
        if (IsDead)
            return;
       
        Health -= Damage;
        HealthBar.fillAmount = Health / MaxHealth;
        if (Health <= 0)
        {
            //Dead
            IsDead = true;
            anim.SetBool("IsDead", IsDead);
            anim.SetTrigger("Death");
            return;
        }

        anim.SetTrigger("Hit");

        OnEnemyDamaged?.Invoke(this, EventArgs.Empty);

    }

    private void EnemyDissolve()
    {
        HealthBar_Stroke.enabled = false;
        Vector4 RGBA = sp.color;
        RGBA -= new Vector4(0, 0, 0, Time.deltaTime);
        transform.GetComponent<SpriteRenderer>().color = RGBA;
        if (RGBA.w <= 0)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundTransform.position, GroundRadius);
        

    }
}
