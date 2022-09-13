using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player_Dash : MonoBehaviour
{


    [Header("Dash")]
    private bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashinPower = 24f;
    private float dashingTime = .5f;
    private float dashingCooldown = 1f;


    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;
    float dashingTimeForImg;
    private CinemachineImpulseSource src;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashingTimeForImg = dashingTime;
        src = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&canDash)
        {
            anim.SetTrigger("Dash");
            StartCoroutine(Dash());
        }
        anim.SetBool("isDashing", isDashing);
    }
    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashinPower, rb.velocity.y);
        src.GenerateImpulse();
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        dashingTimeForImg = dashingTime;


        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
