using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiffrent_attacks : StateMachineBehaviour
{
    
    public float Speed = 2.5f;
    public float attackRange = 3f;
    private bool canSeePlayer = false;

    Transform Player;
    Rigidbody2D rb;
    Enemy enemy;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public float followRange = 0f;
    public bool needToFollow=false;
    int randomAttack = 0;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        randomAttack = Random.Range(1, 3);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (randomAttack == 1)
        {
            attackRange = 6;
        }
        else if (randomAttack == 2)
        {
            attackRange = 2;
        }
        if (Vector2.Distance(Player.position, rb.position) >= attackRange-1f )
        {
            needToFollow = true;

        }
        else if(Vector2.Distance(Player.position, rb.position) <= attackRange - 1f)
        {
            needToFollow = false;
        }
        animator.SetBool("needToFollow", needToFollow);
        
        canSeePlayer = Vector2.Distance(Player.position, rb.position) <= followRange + 3f && !Player.GetComponent<PlayerHealth>().IsDead;
        animator.SetBool("canSeePlayer", canSeePlayer);

        if (Player.GetComponent<PlayerHealth>().IsDead)
            return;


        

        
        enemy.LookAtPlayer();
        Vector2 target = new Vector2(Player.position.x, rb.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);


        if (needToFollow)
        {
            rb.MovePosition(newpos);
            Debug.Log(Vector2.Distance(Player.position, rb.position));
        }
        

        if (Time.time > nextAttackTime && Vector2.Distance(Player.position, rb.position) <= attackRange)
        {

            animator.SetTrigger("Attack" + randomAttack);
            nextAttackTime = Time.time + 1f / attackRate;
            Debug.Log(randomAttack);
        }
        


    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");

    }
}
