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
    private float GroundRadius = 0.5f;


    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBar_Stroke;

    [Header("Components")]
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private LayerMask GroundLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private ParticleSystem Blood_particle;
    private ParticleSystem Death_particle;
    private SpriteRenderer sp;
    private Transform Target;

    [Header("States")]
    private bool IsDead = false;
    private bool IsTargetInRange = false;
    private bool IsGrounded = false;

    private void Start()
    {
        //Refrence
        Blood_particle = GameObject.Find("Blood_particle").GetComponent<ParticleSystem>();
        Death_particle = GameObject.Find("Death_particle").GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        TargetLayer = LayerMask.GetMask("Player");
        GroundLayer = LayerMask.GetMask("Ground");
        Target = GameObject.Find("Player").transform;

        //Set Values
        Health = MaxHealth;
        FollowRange = new Vector3(1,1);
    }

    private void Update()
    {
        if (IsDead)
        {
            EnemyDissolve();
            return;
        }

        IsGrounded = Physics2D.OverlapCircle(transform.position, GroundRadius, GroundLayer);

        
        if (IsTargetInRange)
        {
            FollowPlayer();
        }
    }
   
    //Follow the player
    private void FollowPlayer()
    {

        
    }


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
            Death_particle.Play();
            return;
        }

        anim.SetTrigger("Hit");
        Blood_particle.Play();
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
        
    }
}
