using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{

    public float Speed=2.5f;
    public float attackRange=3f;
    private bool canSeePlayer = false;

    Transform Player;
    Rigidbody2D rb;
    Enemy enemy;
    public bool needToFollow = false;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float followRange = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerHealth.IsDead)
        {
            animator.SetBool("canSeePlayer", false);
            animator.SetBool("needToFollow", false);
            return;
        }
        if (Vector2.Distance(Player.position, rb.position) >= attackRange - 1f)
        {
            needToFollow = true;

        }
        else if (Vector2.Distance(Player.position, rb.position) <= attackRange - 1f)
        {
            needToFollow = false;
        }
        animator.SetBool("needToFollow", needToFollow);

        Vector2 playerPosition = new(Player.position.x, 0);
        Vector2 enemyPosition = new(rb.position.x, 0);

        canSeePlayer = Vector2.Distance(playerPosition, enemyPosition) <= followRange + 3f && !PlayerHealth.IsDead;
        animator.SetBool("canSeePlayer", canSeePlayer);

        enemy.LookAtPlayer();

        //Vector2 target = new Vector2(Player.position.x, Player.position.y);
        //Vector2 newpos = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);

        //if (needToFollow)
        //{
        //    rb.MovePosition(newpos);
        //}
        Vector2 target = new Vector2(Player.position.x, Player.position.y);

        Vector2 dir = (target - (Vector2)rb.transform.position).normalized;
        if (needToFollow)
        {
            rb.velocity = new Vector2(dir.x * Speed, rb.velocity.y);
        }
        if (Time.time > nextAttackTime && Vector2.Distance(Player.position, rb.position) <= attackRange + 1f)
        {
             
            animator.SetTrigger("Attack1");
            nextAttackTime = Time.time + 1f / attackRate;

        }
        


    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
    }
    

}
