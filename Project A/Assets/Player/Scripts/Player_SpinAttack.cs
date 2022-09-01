using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SpinAttack : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Spin");
            }
        }
    }
}
