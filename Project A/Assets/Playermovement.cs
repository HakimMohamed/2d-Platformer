using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private float movement;
    [SerializeField] private float Speed = 20f;

    private bool IsFacingRight = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(movement));


        if (movement > 0 && IsFacingRight==false)
            Flip();
        else if(movement<0&&IsFacingRight==true)
            Flip();

    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void FixedUpdate()
    {
        rb.velocity = Speed * Time.fixedDeltaTime * new Vector3(movement, 0, 0);

    }
}
