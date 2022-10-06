using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject SleepPanel;
    [SerializeField]GameObject SpaceBar;
    GameObject Player;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.GetComponent<Animator>().SetBool("isSleeping", false);
            SleepPanel.GetComponentInChildren<Animator>().SetTrigger("GetUp");
            StopCoroutine(EnableSapaceBar());
            SpaceBar.GetComponentInChildren<UiDestroySelf>().DestroyUi();
            Invoke("EnablePlayerMovement", 1.5f);
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

