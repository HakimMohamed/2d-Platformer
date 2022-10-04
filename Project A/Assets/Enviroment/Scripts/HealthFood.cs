using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFood : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject interactButton;
    PlayerHealth playerhealth;
    public float healthReturnPercentage=.2f;
    Rigidbody2D rb;
    public PhysicsMaterial2D physicMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        playerhealth = Player.GetComponent<PlayerHealth>();
        Invoke("StopBouncing",1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 8f)
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerhealth.Health < playerhealth.MaxHealth)
                {
                    playerhealth.PlayerReceiveHealth(playerhealth.MaxHealth * healthReturnPercentage);
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
