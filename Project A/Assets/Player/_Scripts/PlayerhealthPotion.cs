using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerhealthPotion : MonoBehaviour
{
    public int timesTousePotions = 3;
    private float healthReturned = 30;

    private bool isDrinking = false;
    private float timeToConsume;
    [SerializeField] private float timeToConsumeMax = 1f;
    PlayerHealth PlayerHealth;
    Animator anim;
    Playermovement playermovement;
    float health;
    float maxHealth;
    [SerializeField] TextMeshProUGUI PotionsAvailbe;

    private void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        playermovement = GetComponent<Playermovement>();
        anim = GetComponent<Animator>();
        timeToConsume = timeToConsumeMax;
    }

    // Update is called once per frame
    void Update()
    {

        health = PlayerHealth.Health;
        maxHealth = PlayerHealth.MaxHealth;
        PotionsAvailbe.text = timesTousePotions.ToString() + "x";
        anim.SetBool("isDrinking", isDrinking);

        if (Input.GetKeyDown(KeyCode.Q) && timesTousePotions > 0 && health < maxHealth)
        {
            isDrinking = true;
            playermovement.MoveSpeed = 120f;
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
            PlayerHealth.Health += healthReturned;
        }
        else if (healthReturned + health > maxHealth)
        {
            PlayerHealth.Health = maxHealth;
        }
        playermovement.MoveSpeed = playermovement.DefaultMoveSpeed;
        timesTousePotions -= 1;
    }
}

