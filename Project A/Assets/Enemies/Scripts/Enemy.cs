using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    bool isSearching = false;
    [SerializeField] Vector2 RandomPatrolTime;

    private void Awake()
    {
        //Refrence
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //Set values
       StartCoroutine(RandomPatrol());
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
        
        Vector2 speed = rb.velocity.normalized;

        anim.SetFloat("Speed", Mathf.Abs(speed.x));

        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
            timeToStartPatrol = 0;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    if(!isGettingAttacked)
                    StartCoroutine(StopPatrolingAfterSeconds());
                }
            }

        }

        if (isAgro && Vector2.Distance(transform.position,Player.position)>1f && !isGettingAttacked)
        {
            
            FollowPlayer();
        }

        if (!CanSeePlayer(agroRange)&& IsItTimeToStartPatrol)
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
        yield return new WaitForSeconds(.4f);
        CanFlip = true;
    }
    public IEnumerator HitCoolDown()
    {
        isGettingAttacked = true;
        StopFollowingPlayer();

        yield return new WaitForSeconds(.5f);

        isGettingAttacked = false;
    }
    private IEnumerator StopPatrolingAfterSeconds()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(RandomPatrolTime.x, RandomPatrolTime.y));
        StopFollowingPlayer();
    }
    private IEnumerator RandomPatrol()
    {
        if (isAgro)
            yield break;
        
        int randomPatrol = 0;
        randomPatrol = Mathf.Clamp(randomPatrol, 0, 1);
        randomPatrol = UnityEngine.Random.Range(0, 2);

        if(randomPatrol == 0)
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

        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 7));

        StopPatroling();
        IsItTimeToStartPatrol = true;
    }
    private void StopPatroling()
    {
        rb.velocity = Vector2.zero;
    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isfacingleft)        
            castDist = -distance;
        

        Vector2 endPos = transform.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos);     

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
            else
            {
                val = false;
            }

            

        }
        Debug.DrawLine(transform.position, endPos, Color.red);


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
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void StopFollowingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb.velocity = Vector2.zero;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, WallRadius);
    }
}
