using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class Playermovement : MonoBehaviour
{
    public static bool isUsingVelocity = false;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private CinemachineImpulseSource src;
    private LadderMovement laddermovement;
    [SerializeField] PhysicsMaterial2D NoFriction;
    [Header("Horizontal Movement")]
    public float MoveSpeed = 10f;
    public float DefaultMoveSpeed = 333f;
    [SerializeField] private float WalkMoveSpeed = 120f;
    private Vector2 direciton;
    private bool FacingRight = true;
    private bool LeftCtrl;
    public float speedanimatorMut = 1f;
    public float defualtAnimatorSpeed = 1f;


    [Header("Vertical Movement")]
    public float JumpForce = 10f;
    private float JumpRemember = 0;
    private float JumpRememberTime = .1f;
    private float GroundedRemember = 0;
    private float GroundedRememberTime = 0.1f;
    [SerializeField] private float CutJumpHeight = 1f;

    [Header("Ground Detection")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private float GroundChecker_Radius = .6f;
    [SerializeField] private LayerMask GroundLayer;
    public bool IsGrounded;

    [Header("States")]
    bool isDead;

    PlayerAttack playerattack;
    public int EnemiesKilled = 0;



    [Header("Roll")]
    private bool canRoll = true;
    private bool isRolling = false;
    [SerializeField] private float dashpower = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundChecker = GameObject.Find("GroundChecker").transform;
        src = GetComponent<CinemachineImpulseSource>();

        playerattack = GetComponent<PlayerAttack>();
        laddermovement = GetComponent<LadderMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.IsDead)        
            return;   
        
        IsGrounded =   Physics2D.OverlapCircle(GroundChecker.position, GroundChecker_Radius, GroundLayer);
        bool isJumping = Input.GetKey(KeyCode.Space);
        LeftCtrl = Input.GetKey(KeyCode.LeftControl);

        anim.SetBool("isJumping", isJumping);
        anim.SetBool("IsGrounded", IsGrounded);

        HandleFlipping();





        if (isUsingVelocity)
            return;

        jump_Input_Handler();

        


        float speed = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(speed));
        anim.SetFloat("speedMut", speedanimatorMut);

        //
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
        {
            anim.SetTrigger("Roll");
            StartCoroutine(Roll(dashpower));
            Debug.Log("Roll");
        }
        anim.SetBool("isRolling", isRolling);

        // MovementSpeed Handling

        if (LeftCtrl)
            MoveSpeed = WalkMoveSpeed;
        anim.SetBool("Ctrl", LeftCtrl);

        if (!playerattack.isAttacking  && !PlayerParry.isParry)
        {
            float speedWhileFighting = .4f * DefaultMoveSpeed + DefaultMoveSpeed;
            if (!LeftCtrl && !playerattack.disableAttack)
                MoveSpeed = playerattack.isFighting ? speedWhileFighting : DefaultMoveSpeed;

        }



    }

    private void FixedUpdate()
    {
        
      
        if (isUsingVelocity)
            return;


        MoveCharacter();


    }

    
    private Vector2 Direction() => direciton = new Vector2(Input.GetAxisRaw("Horizontal"), 0);


    private void HandleFlipping()
    {
        if (Direction().x > 0 && !FacingRight)
        {
            Flip(1);
        }
        else if ((Direction().x < 0 && FacingRight))
        {
            Flip(-1);
        }
    }

    void MoveCharacter()
    {
        rb.velocity =   new Vector2(Input.GetAxis("Horizontal") * MoveSpeed * Time.fixedDeltaTime,rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * JumpForce);
        anim.SetTrigger("Jump");
    }
    private void jump_Input_Handler()
    {
        GroundedRemember -= Time.deltaTime;
        if (IsGrounded)
        {
            GroundedRemember = GroundedRememberTime;
        }

        JumpRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpRemember = JumpRememberTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * CutJumpHeight);

        }
        if ((JumpRemember > 0) && (GroundedRemember > 0))
        {
            JumpRemember = 0;
            GroundedRemember = 0;
            Jump();
        }

        //if (rb.velocity.y < _jumpVelocityFalloff || rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        //    rb.velocity += _fallMultiplier * Physics.gravity.y * Time.deltaTime * Vector2.up;
    }

    void Flip(int scaler_)
    {
        FacingRight = !FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = scaler_;
        transform.localScale = scaler;
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundChecker.position, GroundChecker_Radius);

    }
    public IEnumerator Roll(float dashpower)
    {
        canRoll = false;
        isRolling = true;
        Playermovement.isUsingVelocity = true;

        rb.velocity = new Vector2(transform.localScale.x * dashpower, 0);
        yield return new WaitForSeconds(.2f);



        yield return new WaitForSeconds(.5f);
        canRoll = true;
    }
    public void DisableRolling()
    {
        isRolling = false;
        Playermovement.isUsingVelocity = false;

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
