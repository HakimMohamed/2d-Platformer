using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] float EyeRange;
    [SerializeField] float moveSpeed;
    [SerializeField] int attackDamage=10;


    [Header("Refrence Components")]
    [SerializeField] LayerMask playerLayer;
    Transform Player;
    Rigidbody2D rb;
    Animator anim;


    [Header("States")]
    bool isFacingRight=true;
    bool isSearching = false;
    int dirOfPlayer = 0;

    private void Awake()
    {
        //Refrence
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        bool canSeePlayer = CanSeePlayer(EyeRange);


        if (canSeePlayer)
        {
            FollowPlayer();
            isSearching = true;
        }
        else if(canSeePlayer&&isSearching)
        {
            isSearching = false;
            StartCoroutine(KeepSearchingForPlayer());
        }


    }

    private IEnumerator KeepSearchingForPlayer()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        yield return new WaitForSeconds(4f);
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false ;
        
        Vector2 endPos = transform.position + Vector3.right * distance;
        Vector2 startPos = transform.position + Vector3.left * distance;

        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos);     

        if (hit.collider!=null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                Debug.DrawLine(startPos, endPos, Color.blue);
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                val = false;
            }
            
        }
        else
        {
            Debug.DrawLine(startPos, endPos, Color.red);
        }


        return val;
    }
    private void FollowPlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            Flip(1);
        }
        else if(transform.position.x > Player.position.x)
        {
            Flip(-1);
        }

        rb.velocity = new Vector2(dirOfPlayer*moveSpeed, rb.velocity.y);

    }
    void Flip(int dir)
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = dir;
        transform.localScale = scaler;
        dirOfPlayer = dir;
    }
    private void StopFollowingPlayer()
    {
        
        rb.velocity = Vector2.zero;
        
    }

    

    
}
