using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MeeleEnemy : MonoBehaviour
{
    [HideInInspector] public event EventHandler OnEnemyDamaged;
    public float Health;
    public float MaxHealth;



    [Header("Components")]
    private Animator anim;
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBar_Stroke;
    private SpriteRenderer sp;




    [Header("States")]
    private bool IsDead = false;
    private ParticleSystem Blood;


    private void Start()
    {
        Health = MaxHealth;
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        Blood = GetComponentInChildren<ParticleSystem>();
        
    }

    private void Update()
    {
        anim.SetBool("IsDead", IsDead);
        if (IsDead)
            EnemyDissolve();
    }
   
    public void EnemyReceiveDamage(float Damage)
    {
        if (IsDead)
            return;
       


        Health -= Damage;
        HealthBar.fillAmount = Health / MaxHealth;
        Blood.Play();
        if (Health <= 0)
        {
            //Dead
            IsDead = true;
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

}
