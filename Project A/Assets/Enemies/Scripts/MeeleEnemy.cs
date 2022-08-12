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
    private Image HealthBar;
    private SpriteRenderer sp;
    [Header("States")]
    private bool IsDead = false;


    private void Start()
    {
        Health = MaxHealth;
        anim = GetComponent<Animator>();
        HealthBar = GetComponentInChildren<Image>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetBool("IsDead", IsDead);
        if (IsDead)
            EnemyDissolve();
    }
   
    private void EnemyDissolve()
    {
        Vector4 RGBA = sp.color;
        RGBA -= new Vector4(0, 0, 0, Time.deltaTime);
        transform.GetComponent<SpriteRenderer>().color = RGBA;
        if (RGBA.w <= 0)
            Destroy(gameObject);
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
            anim.SetTrigger("Death");
            return;
        }

        anim.SetTrigger("Hit");

        OnEnemyDamaged?.Invoke(this, EventArgs.Empty);

    }
}
