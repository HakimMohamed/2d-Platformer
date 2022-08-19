using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{


    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    [Header("Refrence Components")]
    Transform Player;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] LayerMask playerLayer;

    [Header("States")]
    bool isfacingleft;
    private bool isGettingAttacked = false;
    private float timeToStartPatrol;
    [SerializeField]private float PatrolTime=5f;
    private bool  IsItTimeToStartPatrol;
    private bool isTouchingTheWall = false;
    [SerializeField] private float WallRadius = 0.5f;
    [SerializeField] private LayerMask WallLayer;
    bool CanFlip = true;
    bool isAgro = false;
    [SerializeField] Vector2 RandomPatrolTime;

    private void Awake()
    {
        //Refrence
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //Set values
       timeToStartPatrol = 0f;
       IsItTimeToStartPatrol = false;
    }

    void Update()
    {
        isTouchingTheWall = Physics2D.OverlapCircle(transform.position, WallRadius, WallLayer);

        if (isTouchingTheWall&&CanFlip)
        {
            
            StartCoroutine(FlipOnce());
            
        }
        
        float speed = rb.velocity.normalized.x;

        anim.SetFloat("Speed", Mathf.Abs(speed));
        Debug.Log(CanSeePlayer(agroRange));
        if (CanSeePlayer(agroRange))
        {
            IsItTimeToStartPatrol = false;
            isAgro = true;
            timeToStartPatrol = 0;
        }
        else
        {
            if (isAgro)
            {
                
                if(!isGettingAttacked)
                StartCoroutine(StopPatrolingAfterSeconds());
                
            }

        }

        if (isAgro && Vector2.Distance(transform.position,Player.position)>.7f && !isGettingAttacked)
        {
            
            FollowPlayer();
        }

        if (isAgro && IsItTimeToStartPatrol)
        {
            timeToStartPatrol += Time.deltaTime;
            if(timeToStartPatrol> PatrolTime)
            {
                StartCoroutine(RandomPatrol());
            }
        }


    }
    private IEnumerator FlipOnce()
    {
        transform.localScale = new Vector2(transform.localScale.x*-1, 1);
        CanFlip = false;

        if (transform.localScale.x > 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            isfacingleft = false;
        }
        else if (transform.localScale.x < 1)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            isfacingleft = true;
        }
        yield return new WaitForSeconds(.2f);
        CanFlip = true;
    }
    public IEnumerator AttackCooldown()
    {
        StopFollowingPlayer();
        isAgro = false;
        yield return new WaitForSeconds(.5f);
        isAgro = true;

    }
    public IEnumerator HitCoolDown()
    {
        isGettingAttacked = true;
        StopFollowingPlayer();
        isAgro = false;

        yield return new WaitForSeconds(.5f);
        isAgro = true;

        isGettingAttacked = false;
    }
    private IEnumerator StopPatrolingAfterSeconds()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(RandomPatrolTime.x, RandomPatrolTime.y));
        StopFollowingPlayer();
    }
    private IEnumerator RandomPatrol()
    {
        if (!isAgro)
        {


            int randomPatrol = 0;
            randomPatrol = Mathf.Clamp(randomPatrol, 0, 1);
            randomPatrol = UnityEngine.Random.Range(0, 2);

            if (randomPatrol == 0)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                isfacingleft = false;
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                isfacingleft = true;
            }

            timeToStartPatrol = 0f;
            IsItTimeToStartPatrol = false;
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 7));
        if (!IsItTimeToStartPatrol)
        {
            rb.velocity = Vector2.zero;
        }
        IsItTimeToStartPatrol = true;
        
        

    }
    
    bool CanSeePlayer(float distance)
    {
        bool val=false ;
        float castDist = distance;

        if (isfacingleft)        
            castDist = -distance;
        

        Vector2 endPos = transform.position + Vector3.right * castDist;
        Vector2 startPos = transform.position + Vector3.left * castDist;

        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos);     

        if (hit.collider!=null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }         
            else if(hit.collider.CompareTag("Wall")&&!isAgro)
            {
                if (timeToStartPatrol > PatrolTime)
                {
                    StartCoroutine(RandomPatrol());

                }
                
                val = false;
            }
            

            

        }
        Debug.DrawLine(startPos, endPos, Color.red);


        return val;
    }
    private void FollowPlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            isfacingleft = false;
        }
        else if(transform.position.x > Player.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            isfacingleft = true;
        }
        
    }

    private void StopFollowingPlayer()
    {
        
        rb.velocity = Vector2.zero;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, WallRadius);
    }

    
}
