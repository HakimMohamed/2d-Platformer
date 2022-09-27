using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    Playermovement playermovement;
    Rigidbody2D rb;
    Animator anim;
    private float jumpTimer=1f;
    private bool canJump = true;
    void Start()
    {
        playermovement = GetComponent<Playermovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playermovement.IsGrounded && Input.GetKeyDown(KeyCode.Space)&&canJump)
        {
            canJump = false;
            Jump();
        }
        if (playermovement.isSliding && !playermovement.IsGrounded && Input.GetKeyDown(KeyCode.Space)&&canJump)
        {
            canJump = false;
            Jump();
        }
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                jumpTimer = 1f;
                canJump = true;
            }
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * playermovement.JumpForce*1.3f);
        anim.SetTrigger("SecondJump");
    }
}
