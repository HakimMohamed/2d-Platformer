using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject SleepPanel;
    [SerializeField]GameObject SpaceBar;
    GameObject Player;
    public float SlowDownFactor;
    private bool attacked=false;
    [SerializeField] GameObject MouseClick;
    [SerializeField] GameObject PlayerHealth;
    [SerializeField] GameObject PlayerBones;
    GameObject enemy;
    private void Awake()
    {
        SpaceBar.SetActive(false);
        EnableSleepPanel();
        Player = GameObject.Find("Player");
        Player.GetComponent<Animator>().SetTrigger("Sleep");
        Player.GetComponent<Animator>().SetBool("isSleeping",true);
        Player.GetComponent<Playermovement>().enabled = false;
        StartCoroutine(EnableSapaceBar());

    }
    void Start()
    {
        enemy = GameObject.Find("EnemyWithSlowSign");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.GetComponent<Animator>().SetBool("isSleeping", false);
            SleepPanel.GetComponentInChildren<Animator>().SetTrigger("GetUp");
            StopCoroutine(EnableSapaceBar());
            if(SpaceBar!=null)
                SpaceBar.GetComponentInChildren<UiDestroySelf>().DestroyUi();
            Invoke("EnablePlayerMovement", 1.5f);
        }
        if (enemy != null)
        {
            if (enemy.GetComponent<SlowSign>().enemyIsTooClose)
            {
                Player.GetComponent<PlayerAttack>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    attacked = true;

                }
                if (!attacked)
                {
                    Time.timeScale = SlowDownFactor;
                    MouseClick.SetActive(true);

                }
                else
                {
                    Time.timeScale = 1f;

                }
            }
        }
        if (attacked || enemy == null)
        {
            MouseClick.SetActive(false);
            Time.timeScale = 1f;
            PlayerHealth.SetActive(true);
            PlayerBones.SetActive(true);
        }

    }

    private IEnumerator EnableSapaceBar()
    {
        yield return new WaitForSeconds(4f);
        if(SpaceBar!=null)
            SpaceBar.SetActive(true);

    }
    private void EnableSleepPanel()
    {
        SleepPanel.SetActive(true);

    }
    private void EnablePlayerMovement()
    {
        Player.GetComponent<Playermovement>().enabled = true;
    }
}

