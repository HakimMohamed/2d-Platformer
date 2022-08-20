using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Playermovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] TrailRenderer tr;
    private CinemachineImpulseSource src;

    [Header("Horizontal Movement")]
    public float MoveSpeed = 10f;
    private Vector2 direciton;
    private bool FacingRight = true;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashinPower=24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown =1f;


    [Header("Vertical Movement")]
    public float JumpForce = 10f;

    [Header("Ground Detection")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private float GroundChecker_Radius = .6f;
    [SerializeField] private LayerMask GroundLayer;
    private bool IsGrounded;

    [Header("States")]
    bool isDead;
    public bool isAttacking=false;

    [SerializeField] private float _jumpVelocityFalloff = 8;
    [SerializeField] private float _fallMultiplier = 7;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundChecker = GameObject.Find("GroundChecker").transform;
        isDead = GetComponent<PlayerHealth>().IsDead;
        src = GetComponent<CinemachineImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            GetComponent<Playermovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            return;
        }
        
            
        if (isDashing)
            return;

        isDead = GetComponent<PlayerHealth>().IsDead;

        isGroundedHandler();

        Air_AnimationsHandler();



        jump_Input_Handler();


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
            return;
        MoveCharacter(Direction().x);


    }


    private Vector2 Direction() =>direciton = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
    

   
    private void isGroundedHandler()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker_Radius, GroundLayer);
        anim.SetBool("IsGrounded", IsGrounded);
    }
    private void Air_AnimationsHandler()
    {
        float Yvelocity = rb.velocity.y;
        Yvelocity = Mathf.Clamp(Yvelocity, 0, 1);
        anim.SetFloat("Yvelocity", Yvelocity, 0.1f, Time.deltaTime);
    }

    void MoveCharacter(float horizontal)
    {
       
        
        rb.velocity = new Vector2(horizontal * Time.deltaTime * MoveSpeed, rb.velocity.y);
        var speed = rb.velocity.normalized.x;
        anim.SetFloat("Speed", Mathf.Abs(speed));

        if (horizontal > 0 && !FacingRight)
        {
            Flip(1);
        }
        else if ((horizontal < 0 && FacingRight))
        {
            Flip(-1);
        }
        

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x,Vector2.up.y*JumpForce);
        anim.SetTrigger("Jump");
    }
    private void jump_Input_Handler()
    {
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            Jump();
            Debug.Log("aasd");
        }

        if (rb.velocity.y < _jumpVelocityFalloff || rb.velocity.y > 0 && !Input.GetKey(KeyCode.C))
            rb.velocity += _fallMultiplier * Physics.gravity.y * Vector2.up * Time.deltaTime;

    }

    void Flip(int scaler_)
    {
        FacingRight = !FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = scaler_;
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
        src.GenerateImpulse();

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

    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * transform.localScale.x, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(transform.localScale.x, 1, 1);
        }
    }
}
