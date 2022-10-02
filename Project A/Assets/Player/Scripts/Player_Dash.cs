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
    PlayerHealth playerhealth;
    private float manaUsage=10f;
    float defualtGravityScale;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defualtGravityScale = rb.gravityScale;
        anim = GetComponent<Animator>();
        dashingTimeForImg = dashingTime;
        src = GetComponent<CinemachineImpulseSource>();
        playerhealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&canDash&& playerhealth.PlayerUseMana(manaUsage))
        {
            anim.SetTrigger("Dash");
            StartCoroutine(Dash(transform.localScale.x,dashinPower));

        }
        anim.SetBool("isDashing", isDashing);
    }
    public IEnumerator Dash(float dir,float dashpower)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = defualtGravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dir * dashpower, 0);
        src.GenerateImpulse();
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        dashingTimeForImg = dashingTime;


        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
