using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SlowSign : MonoBehaviour
{
    public bool enemyIsTooClose = false;
    bool GotClose = false;

    private static event EventHandler OnEnemyTooClose;
    private static event EventHandler OnAttacked;

    private float SlowDownFactor=0.4f;

    Transform Player;
    [SerializeField] GameObject MouseClick;
    [SerializeField] GameObject PlayerHealth;
    [SerializeField] GameObject PlayerBones;
    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        OnEnemyTooClose += SlowSign_OnEnemyTooClose;
        OnAttacked += SlowSign_OnAttacked;
    }

    private void SlowSign_OnAttacked(object sender, EventArgs e)
    {
        OnAttacked -= SlowSign_OnAttacked;
        MouseClick.SetActive(false);
        Time.timeScale = 1f;
        PlayerHealth.SetActive(true);
        PlayerBones.SetActive(true);
    }


    private void SlowSign_OnEnemyTooClose(object sender, EventArgs e)
    {
        OnEnemyTooClose -= SlowSign_OnEnemyTooClose;
        Time.timeScale = SlowDownFactor;
        MouseClick.SetActive(true);
        Player.GetComponent<PlayerAttack>().enabled = true;     
        
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) < 7f&& !GotClose )
        {
            GotClose = true;
            OnEnemyTooClose?.Invoke(this, EventArgs.Empty);

        }
        if (GotClose)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnAttacked?.Invoke(this, EventArgs.Empty);
                GotClose = false;
            }
        }
    }

}
