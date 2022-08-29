using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerhealthPotion : MonoBehaviour
{
    private int timesTousePotions = 3;
    private float healthReturned=30;

    private bool isDrinking=false;
    private float timeToConsume ;
    [SerializeField]private float timeToConsumeMax = 1f;
    PlayerHealth playerhealth;
    Animator anim;
    Playermovement playermovement;
    float health;
    float maxHealth;

    private void Awake()
    {
        playerhealth = GetComponent<PlayerHealth>();
        playermovement = GetComponent<Playermovement>();
        anim = GetComponent<Animator>();
        timeToConsume = timeToConsumeMax;
    }

    // Update is called once per frame
    void Update()
    {
        health = playerhealth.Health;
        maxHealth = playerhealth.MaxHealth;
        
        anim.SetBool("isDrinking", isDrinking);

        if (Input.GetKeyDown(KeyCode.E)&&timesTousePotions>0&& health<maxHealth)
        {
            isDrinking = true;
            playermovement.MoveSpeed = playermovement.WalkMoveSpeed;
        }
        if (isDrinking)
        {
            timeToConsume -= Time.deltaTime;
            if (timeToConsume <= 0)
            {
                Heal();
                timeToConsume = timeToConsumeMax;
                isDrinking = false;
            }
        }


    }
    public void Heal()
    {
        AudioManager_PrototypeHero.instance.PlaySound("heal");
        if (healthReturned + health < maxHealth)
        {
            playerhealth.Health += healthReturned;
        }
        else if (healthReturned + health > maxHealth)
        {
            playerhealth.Health = maxHealth;
        }
        playermovement.MoveSpeed = playermovement.DefaultMoveSpeed;
        timesTousePotions -= 1;
    }
}
