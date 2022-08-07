using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Horizontal Movement")]
    public float MoveSpeed = 10f;
    public Vector2 direciton;
    private bool FacingRight = true;

    



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direciton = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        MoveCharacter(direciton.x);
    }


    void MoveCharacter(float horizontal)
    {
        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (horizontal > 0 && !FacingRight || (horizontal < 0 && FacingRight))
        {
            Flip();
        }


    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }



}
