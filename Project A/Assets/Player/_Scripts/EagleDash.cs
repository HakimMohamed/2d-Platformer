using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EagleDash : MonoBehaviour
{

    [Header("Dash")]
    private bool canDash = true;
    public static bool isEagle;
    [SerializeField] private float dashinPower = 24f;
    private float dashingTime = .5f;
    private float dashingCooldown = 1f;


    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;
    float dashingTimeForImg;
    private CinemachineImpulseSource src;
    float originalGravity;
    //bool startdecrementingDashTime=false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashingTimeForImg = dashingTime;
        src = GetComponent<CinemachineImpulseSource>();
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isFalcon", isEagle);

        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            anim.SetTrigger("Falcon");
            isEagle = true;
            Playermovement.isUsingVelocity = true;

        }

    }
    public IEnumerator Dash()
    {
        canDash = false;
        rb.velocity = new Vector2(transform.localScale.x * dashinPower,0);
        Playermovement.isUsingVelocity = true;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isEagle = false;
        dashingTimeForImg = dashingTime;
        Playermovement.isUsingVelocity = false;


        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void startDash()
    {
        StartCoroutine(Dash());
    }
}
