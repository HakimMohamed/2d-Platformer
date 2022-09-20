using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFood : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject interactButton;
    PlayerHealth playerhealth;
    public float healthReturnPercentage=.2f;
    void Start()
    {
        Player = GameObject.Find("Player");
        playerhealth = Player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 3f)
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerhealth.PlayerReceiveHealth (playerhealth.MaxHealth * healthReturnPercentage);
                Destroy(gameObject);
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }
}
