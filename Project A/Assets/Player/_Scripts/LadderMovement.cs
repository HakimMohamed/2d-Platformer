using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    //private float vertical;
    //private float speed = 8f;
    //private bool isLadder;
    //public bool isClimbing;
    //private float defualtGravity;
    //private Rigidbody2D rb;
    //private Animator anim;
    //private Playermovement playermovement;
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    defualtGravity = rb.gravityScale;
    //    playermovement = GetComponent<Playermovement>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    anim.SetBool("IsClimbing", isClimbing);
    //    vertical = Input.GetAxis("Vertical");

    //    if (isLadder && Mathf.Abs(vertical)>0f)
    //    {
    //        isClimbing = true;
    //    }
    //    if (isClimbing && !playermovement.isSliding)
    //    {
    //        rb.gravityScale = 0f;
    //        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
    //    }
    //    else
    //    {
    //        if (!playermovement.isSliding)
    //            rb.gravityScale = defualtGravity;
    //    }
    //}

    //private void FixedUpdate()
    //{

       
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ladder"))
    //    {
    //        isLadder = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ladder"))
    //    {
    //        isLadder = false;
    //        isClimbing = false;
    //    }
    //}
}
