using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnSpacePressed += GameManager_OnSpacePressed;
        GameManager.OnSleeping += GameManager_OnSleeping;
        PlayerHealth.OnPlayerDied += DisablePlayerMovements_OnPlayerDied;
    }

    private void DisablePlayerMovements_OnPlayerDied(object sender, System.EventArgs e)
    {
        PlayerHealth.Health = 0;
        PlayerHealth.IsDead = true;
        GetComponent<Animator>().SetTrigger("Death");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Playermovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
    }

    private void GameManager_OnSleeping(object sender, System.EventArgs e)
    {
        GetComponent<Animator>().SetTrigger("Sleep");
        GetComponent<Animator>().SetBool("isSleeping", true);
        GetComponent<Playermovement>().enabled = false;

    }

    private void GameManager_OnSpacePressed(object sender, System.EventArgs e)
    {
        GetComponent<Animator>().SetBool("isSleeping", false);
        Invoke("EnableMovement", 1f);

    }
    public void EnableMovement()
    {
        GetComponent<Playermovement>().enabled = true;
    }
}
