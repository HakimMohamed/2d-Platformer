using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSign : MonoBehaviour
{
    Transform Player;
    public bool enemyIsTooClose = false;
    private void Start()
    {
        Player = GameObject.Find("Player").transform;

    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) < 10f)
        {
            enemyIsTooClose = true;
        }
    }
}
