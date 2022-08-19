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


    [Header("Components")]
    private Animator anim;
    public bool IsDead = false;
    private void Awake()
    {
        //Refrence

        anim = GetComponent<Animator>();

        //Set Values
        Health = MaxHealth;

    }
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            PlayerReceiveDamage(20f);
        }
    }

    public void PlayerReceiveDamage(float Damage)
    {
        Health -= Damage;
        HealthBar.fillAmount = Health / MaxHealth;
        if (Health <= 0)
        {
            //Dead
            IsDead = true;
            anim.SetTrigger("Death");

            return;
        }

        anim.SetTrigger("Hurt");

    }
}
