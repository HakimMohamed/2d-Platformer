using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthFood : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject interactButton;
    PlayerHealth PlayerHealth;
    public float healthReturnPercentage = .2f;
    Rigidbody2D rb;
    public PhysicsMaterial2D physicMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        Invoke("StopBouncing", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 8f)
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (PlayerHealth.Health < PlayerHealth.MaxHealth)
                {
                    PlayerHealth.PlayerReceiveHealth(PlayerHealth.MaxHealth * healthReturnPercentage);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }
    private void StopBouncing()
    {
        rb.sharedMaterial = physicMaterial;
    }
}
