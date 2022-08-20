using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Transform Player;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LookAtPlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            Flip(1);
        }
        else if (transform.position.x > Player.position.x)
        {
            Flip(-1);
        }


    }
    void Flip(int dir)
    {
        Vector3 scaler = transform.localScale;
        scaler.x = dir;
        transform.localScale = scaler;
    }
}
