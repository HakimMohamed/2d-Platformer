using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    Animator anim;
    public static bool isParry = false;
    public static bool canParry = true;
    private float canParryTimer=2f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        isParry = Input.GetMouseButton(1)&&canParry;
        anim.SetBool("isParry", isParry);

        if (!canParry)
        {
            canParryTimer-=Time.deltaTime;
            if (canParryTimer <= 0)
            {
                canParry = true;
                canParryTimer = 2f;
            }
        }
        if (isParry)
        {
            GetComponent<Playermovement>().MoveSpeed = .1f * GetComponent<Playermovement>().DefaultMoveSpeed;

        }
    }
}
