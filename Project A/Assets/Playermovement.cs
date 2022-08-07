using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Horizontal Movement")]
    public float MoveSpeed = 10f;
    private Vector2 direciton;
    private bool FacingRight = true;

    [Header("Vertical Movement")]
    public float JumpForce = 10f;
    public float JumpTimeCounter;
    public float JumpTime;
    private bool IsJumping;

    [Header("Ground Detection")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private float GroundChecker_Radius = .6f;
    private bool IsGrounded;
    private string GroundLayer = "Ground";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundChecker = GameObject.Find("GroundChecker").transform;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker_Radius, LayerMask.GetMask(GroundLayer));
        anim.SetBool("IsGrounded", IsGrounded);
        float Yvelocity = rb.velocity.y;
        Yvelocity = Mathf.Clamp(Yvelocity, 0, 1);
        anim.SetFloat("Yvelocity", Yvelocity,0.1f,Time.deltaTime);

        direciton = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            IsJumping = true;
            Jump();
            JumpTimeCounter = JumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && IsJumping)
        {
            if (JumpTimeCounter > 0)
            {
                Jump();
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
            IsJumping = false;
    }
    private void FixedUpdate()
    {
        MoveCharacter(direciton.x);
        
        
    }


    void MoveCharacter(float horizontal)
    {
        rb.velocity = new Vector2(horizontal*Time.deltaTime*MoveSpeed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (horizontal > 0 && !FacingRight || (horizontal < 0 && FacingRight))
        {
            Flip();
        }


    }

    void Jump()
    {
        rb.velocity = Vector2.up * JumpForce;
    }
    
    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundChecker.position, GroundChecker_Radius);
    }


}
