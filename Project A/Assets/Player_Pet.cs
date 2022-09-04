using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pet : MonoBehaviour
{
    Transform Panda;
    [SerializeField]float Distance_Between_Player_and_Panda;
    Animator panda_Animator;
    float pettingTime;
    float pettingTimeMax=3f;
    bool isLaughing;

    Playermovement playermovement;
    void Start()
    {
        isLaughing = false;
        Panda = GameObject.Find("Panda").transform;
        panda_Animator = Panda.GetComponent<Animator>();
        pettingTime = pettingTimeMax;
        playermovement = GetComponent<Playermovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Panda.position)< Distance_Between_Player_and_Panda)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Petting");
                StartCoroutine(Petting());
            }
            
        }
        
    }
    private IEnumerator Petting()
    {
        isLaughing = true;
        panda_Animator.SetBool("isLaughing", isLaughing);
        playermovement.enabled = false;
        yield return new WaitForSeconds(pettingTime);
        isLaughing = false;
        panda_Animator.SetBool("isLaughing", false);
        playermovement.enabled = true;

    }
}
