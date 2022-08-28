using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EagleDash : MonoBehaviour
{
    [SerializeField] private Image EagleDash_img;

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

    //bool startdecrementingDashTime=false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashingTimeForImg = dashingTime;
        EagleDash_img.fillAmount = 1f;
        src = GetComponent<CinemachineImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isDashing", isDashing);

        if (Input.GetKeyDown(KeyCode.F) && canDash)
        {
            anim.SetTrigger("Falcon");
            isDashing = true;

        }
        if (isDashing)
        {
            dashingTimeForImg -= Time.deltaTime;
            EagleDash_img.fillAmount = dashingTimeForImg / dashingTime;
        }
        else
        {
            EagleDash_img.fillAmount +=Time.deltaTime;
        }

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
        EagleDash_img.fillAmount = 1f;
    }
    public void startDash()
    {
        StartCoroutine(Dash());
    }
}
