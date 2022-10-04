using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject interactButton;
    [SerializeField] int DoorNumber = 0;
    void Start()
    {
        Player = GameObject.Find("Player");
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 3f)
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Player.GetComponent<Playermovement>().enabled = false;
                SceneManager.LoadScene(DoorNumber);
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }
    
}
