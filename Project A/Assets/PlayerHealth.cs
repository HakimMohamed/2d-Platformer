using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{


    [Header("Properties")]
    [HideInInspector] public float Health;
    [SerializeField] float MaxHealth;

    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBar_Stroke;


    [Header("Components")]
    private Animator anim;
    private bool IsDead = false;
    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();

        //Set Values
        Health = MaxHealth;

    }


    public void PlayerReceiveDamage(float Damage)
    {
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

    }
}
