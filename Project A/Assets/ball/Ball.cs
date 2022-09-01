using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed=10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity += new Vector2(BallSpeed*Time.deltaTime, rb.velocity.y);

    }
}
