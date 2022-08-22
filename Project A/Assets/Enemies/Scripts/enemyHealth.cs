using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHealth : MonoBehaviour
{


    [Header("Properties")]
    [HideInInspector] public float Health;
    [SerializeField] float MaxHealth;


    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBar_Stroke;

    [Header("Components")]
    private Animator anim;
    private SpriteRenderer sp;
    private Transform Blood;

    [Header("States")]
    private bool IsDead = false;

    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        

        //Set Values
        Health = MaxHealth;
        Blood = GameAssets.instance.BloodVFX;

    }

    private void Update()
    {

        if (IsDead)
        {
            EnemyDissolve();
            return;
        }

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
            EnemyBlood();
            //StartCoroutine( DoSlowMotion());
            return;
        }
        anim.SetTrigger("Hit");
        EnemyBlood();
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

    private void EnemyBlood()
    {
        Instantiate(Blood, transform.position, Quaternion.identity);

       
    }
    
}
