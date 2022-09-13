using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealth : MonoBehaviour
{

    [Header("Properties")]
    [HideInInspector] public float Health;
    [SerializeField] float MaxHealth;


   

    [Header("Components")]
    private Animator anim;
    private SpriteRenderer sp;
    private Transform Blood;
    private bool IsDead = false;




    Rigidbody2D rb;
    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //Set Values
        Health = MaxHealth;
        Blood = GameAssets.instance.BloodVFX;

    }

    private void Update()
    {

        if (IsDead)
        {
            BarrelDissolve();
            return;
        }

    }

    //Follow the player



    public void BarrelReceiveDamage(float Damage)
    {
        

        Health -= Damage;

        if (Health <= 0&&!IsDead)
        {
            //Dead
            IsDead = true;    

            anim.SetTrigger("Death");
            
            
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            

            //StartCoroutine( DoSlowMotion());
            return;
        }


    }

    private void BarrelDissolve()
    {
        Vector4 RGBA = sp.color;
        RGBA -= new Vector4(0, 0, 0, Time.deltaTime);
        transform.GetComponent<SpriteRenderer>().color = RGBA;
        if (RGBA.w <= 0)
            Destroy(gameObject);
    }

    
}
