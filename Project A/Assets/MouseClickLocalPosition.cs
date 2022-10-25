using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickLocalPosition : MonoBehaviour
{
    Transform Player;
    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (Player.transform.localScale.x>0)
        {
            transform.localScale = new Vector2(20, 20);
        }
        else
        {
            transform.localScale = new Vector2(-20, 20);

        }
    }
}
