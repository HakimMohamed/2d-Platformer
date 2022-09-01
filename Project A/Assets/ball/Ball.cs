using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed=10f;
    private Rigidbody2D rb;
    bool IsBallMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            IsBallMoving = true;
        }
    }
    private void FixedUpdate()
    {
        if (IsBallMoving)
        {


            rb.velocity += new Vector2(BallSpeed * Time.deltaTime, rb.velocity.y);
        }

    }
}
