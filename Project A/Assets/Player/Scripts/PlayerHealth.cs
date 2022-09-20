using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{


    [Header("Properties")]
    [HideInInspector] public float Health;
    [SerializeField] public float MaxHealth;
    [HideInInspector] public float Mana;
    public float MaxMana;
    [Header("UI Components")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ManaBar;


    [Header("Components")]
    private Animator anim;
    public bool IsDead = false;
    Rigidbody2D rb;
    private CinemachineImpulseSource src;
    [SerializeField]private TextMeshProUGUI healthNumber_UI;
    [SerializeField] private TextMeshProUGUI ManaNumber_UI;

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

        if(Mana<MaxMana)
            Mana += Time.deltaTime;

        if (Mana > MaxMana)
            Mana = MaxMana;
    }

    public void PlayerReceiveDamage(float Damage)
    {
        if (!IsDead)
        {
            Health -= Damage;
            if (Health <= 0 )
            {
                //Dead
                Health = 0;
                IsDead = true;
                anim.SetTrigger("Death");
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;

                CameraShake();
                return;
            }
            CameraShake();
            anim.SetTrigger("Hurt");
        }
    }
    public void PlayerReceiveHealth(float amount)
    {
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
    public void PlayerUseMana(float amount)
    {
        Debug.Log("xx");
        Mana -= amount;
        

    }
    public void CameraShake()
    {
        src.GenerateImpulse();

    }
}
