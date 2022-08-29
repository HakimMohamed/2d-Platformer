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

    [Header("UI Components")]
    [SerializeField] private Image HealthBar;


    [Header("Components")]
    private Animator anim;
    public bool IsDead = false;
    Rigidbody2D rb;
    private CinemachineImpulseSource src;
    [SerializeField]private TextMeshProUGUI healthNumber_UI;
    private void Awake()
    {
        //Refrence
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        src = GetComponent<CinemachineImpulseSource>();
        //Set Values
        Health = MaxHealth;
        healthNumber_UI.text = Health + "/" + MaxHealth;
    }
    private void Update()
    {
        HealthBar.fillAmount = Health / MaxHealth;
        healthNumber_UI.text = Health + "/" + MaxHealth;
    }

    public void PlayerReceiveDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            //Dead
            IsDead = true;
            anim.SetTrigger("Death");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;

            CameraShake();
            return;
        }
        CameraShake();
        anim.SetTrigger("Hurt");

    }

    public void CameraShake()
    {
        src.GenerateImpulse();

    }
}
