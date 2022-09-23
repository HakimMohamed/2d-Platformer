using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    GameObject Player;
    [SerializeField]GameObject TakeButton;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, Player.transform.position) < 3f)
        {
            TakeButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Player.GetComponent<PlayerhealthPotion>().timesTousePotions += 1;
                Destroy(gameObject);
            }
        }
        else
        {
            TakeButton.SetActive(false);
        }
    }
    
}
