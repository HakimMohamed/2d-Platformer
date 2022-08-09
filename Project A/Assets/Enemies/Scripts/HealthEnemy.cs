using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float stratingHealth;
    public float currentHealth { get; private set; }
    private float damage;

    private void Awake()
    {
        currentHealth = stratingHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, stratingHealth);

        if (currentHealth > 0)
        {


            //



            

        }
        if(currentHealth <= 0)
        {
            
        }

        
    {
        
    }
}

  private void ResetHealth()
    {
       //
        
    }
    
}


