using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EagleDash : MonoBehaviour
{

    [Header("Dash")]
    private bool canDash = true;
    public bool isFalcon;
    [SerializeField] private float dashinPower = 24f;
    private float dashingTime = .5f;
    private float dashingCooldown = 1f;


    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;
    float dashingTimeForImg;
    private CinemachineImpulseSource src;

    //bool startdecrementingDashTime=false;
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
        anim.SetBool("isFalcon", isFalcon);

        if (Input.GetKeyDown(KeyCode.F) && canDash)
        {
            anim.SetTrigger("Falcon");
            isFalcon = true;

        }
        

    }
    public IEnumerator Dash()
    {
        canDash = false;
        isFalcon = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashinPower, rb.velocity.y);
        src.GenerateImpulse();
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isFalcon = false;
        dashingTimeForImg = dashingTime;


        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void startDash()
    {
        StartCoroutine(Dash());
    }
}
