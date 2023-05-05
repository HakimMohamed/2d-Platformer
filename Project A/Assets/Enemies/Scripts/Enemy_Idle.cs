using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : StateMachineBehaviour
{

    private bool canSeePlayer = false;
    Transform Player;
    Rigidbody2D rb;
    public float followRange = 0f;
    [SerializeField] LayerMask playerLayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Vector2 playerPosition = new(Player.position.x, 0);
        Vector2 enemyPosition = new(rb.position.x, 0);

        canSeePlayer = Vector2.Distance(playerPosition, enemyPosition) <= followRange + 3f&& !PlayerHealth.IsDead;
        animator.SetBool("canSeePlayer", canSeePlayer);

        if (PlayerHealth.IsDead)
            return;


    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
