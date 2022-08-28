using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class PlayerAura : MonoBehaviour
{

    Playermovement playermovement;
    PlayerAttack playerattack;

    [Header("Upgrades during Ability")]
    private float speed;
    [SerializeField]private float aura_MoveSpeed=.1f;
    private float attackSpeed;
    private float duration;
    private float durationMax=6f;
    private float coolDown;
    [SerializeField]private float coolDownMax = 3f;
    [SerializeField]private float anim_Speed = 1.5f;
    [Header("UI")]
    [SerializeField] private Image aura_Image;
    [SerializeField] private Animator aura_anim;
    [SerializeField] private Animator aura_Panel;


    [Header("VFX")]
    [SerializeField]private GameObject Aura;


    [Header("States")]
    private bool canAura;
    public bool isInAura;
    private CinemachineImpulseSource src;


    private void Awake()
    {
        playermovement = GetComponent<Playermovement>();
        playerattack = GetComponent<PlayerAttack>();
        speed = playermovement.DefaultMoveSpeed;
        speed += playermovement.MoveSpeed*aura_MoveSpeed;
        src = GetComponent<CinemachineImpulseSource>();

        // attackSpeed = playerattack.attackRate;
        // Duration = lvl 1 6 seconds  for now

        duration = durationMax;
        coolDown = 0f;
        canAura = true;
        isInAura = false;
    }

    private void Update()
    {
        aura_anim.SetBool("canAura", canAura);

        if (Input.GetKeyDown(KeyCode.V)&&canAura)
        {
            StartCoroutine(AuraEffect());
            aura_Panel.SetTrigger("AuraEffect");
            src.GenerateImpulse();

        }
        if (isInAura)
        {
            duration -= Time.deltaTime;
            aura_Image.fillAmount = duration / durationMax;
        }
        else if (!canAura&&!isInAura)
        {
            coolDown += Time.deltaTime;
            aura_Image.fillAmount = coolDown / coolDownMax;
             
        }
    }

    private IEnumerator AuraEffect()
    {
        Aura.SetActive(true);
        canAura = false;
        isInAura = true;
        playermovement.MoveSpeed = speed;
        //playerattack.attackRate = attackSpeed;
        playermovement.speedanimatorMut = anim_Speed;
        yield return new WaitForSeconds(durationMax);
        
        Aura.SetActive(false);
        isInAura = false;
        duration = durationMax;
        playermovement.MoveSpeed = playermovement.DefaultMoveSpeed;
        //playerattack.attackRate = playerattack.defaultAttackSpeed;
        playermovement.speedanimatorMut = playermovement.defualtAnimatorSpeed;
        ;

        yield return new WaitForSeconds(coolDownMax);
        canAura = true;
        coolDown = 0f;
        aura_Panel.SetTrigger("AuraEffect");

    }
}
