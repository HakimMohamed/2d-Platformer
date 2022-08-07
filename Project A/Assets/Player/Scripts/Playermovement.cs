using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] TrailRenderer tr;

    [Header("Horizontal Movement")]
    public float MoveSpeed = 10f;
    private Vector2 direciton;
    private bool FacingRight = true;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashinPower=24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown =0.5f;


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
        if (isDashing)
            return;

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
            return;
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashinPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundChecker.position, GroundChecker_Radius);
    }


}
