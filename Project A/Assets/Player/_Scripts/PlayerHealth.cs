using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cinemachine;


public class PlayerHealth : MonoBehaviour
{


    [Header("Properties")]
    [HideInInspector] public static float Health;
    [SerializeField] public  float MaxHealth;
    [HideInInspector] public static float Mana;
    public float MaxMana;
    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ManaBar;


    [Header("Components")]
    private Animator anim;
    public static bool IsDead = false;
    Rigidbody2D rb;
    private CinemachineImpulseSource src;
    [SerializeField] private TextMeshProUGUI healthNumber_UI;
    [SerializeField] private TextMeshProUGUI ManaNumber_UI;
    [SerializeField] private Animator ManaBar_anim;
    [SerializeField] public static event EventHandler OnPlayerDied;
    private void Awake()
    {
        //Refrence
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        src = GetComponent<CinemachineImpulseSource>();
        //Set Values
        Health = MaxHealth;
        healthNumber_UI.text = Health + "/" + MaxHealth;

        Mana = MaxMana;
        healthNumber_UI.text = Mana + "/" + MaxMana;

    }

    

    private void Update()
    {
        ManaBar.fillAmount = Mana / MaxMana;
        ManaNumber_UI.text = (int)Mana + "/" + MaxMana.ToString("0");

        HealthBar.fillAmount = Health / MaxHealth;
        healthNumber_UI.text = Health + "/" + MaxHealth;
        anim.SetBool("isDead", IsDead);

        if (Mana < MaxMana)
            Mana += Time.deltaTime * 1.4f;

        if (Mana > MaxMana)
            Mana = MaxMana;
    }
   
    public void PlayerReceiveDamage(float damage)
    {
        if (IsDead)
            return;

        

        if (PlayerParry.isParry && PlayerParry.canParry)
        {
            float knockBackPower = 8f;
            StartCoroutine(Extensions.addForceToPlayer(rb, (int)-transform.localScale.x, knockBackPower));
            anim.SetTrigger("Parryhit");
            PlayerParry.canParry = false;
            return;
        }



        Health -= 20f;

        if (Health <= 0)
        {
            //Dead
            OnPlayerDied?.Invoke(this, EventArgs.Empty);

            return;
        }
        CameraShake();
        anim.SetTrigger("Hurt");

    }
    public void PlayerReceiveHealth(float amount)
    {
        if (IsDead)
            return;
        float health = Health + amount;
        if (health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += amount;
        }

    }
    public bool PlayerUseMana(float amount)
    {
        if (IsDead)
            return false;

        if (Mana - amount >= 0)
        {
            Mana -= amount;
            return true;
        }
        else
        {
            notEnoughMana();
            return false;
        }

    }
    private void notEnoughMana()
    {
        if (IsDead)
            return;
        ManaBar_anim.SetTrigger("notEnough");
        CameraShake();
    }
    public void CameraShake()
    {
        src.GenerateImpulse();

    }
}
