using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRun : MonoBehaviour
{
    Transform Player;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Player.position.x>transform.position.x-33f)
            GetComponent<Rigidbody2D>().velocity = Random.Range(100f,200f) * Time.fixedDeltaTime * Vector2.left;

    }
}
