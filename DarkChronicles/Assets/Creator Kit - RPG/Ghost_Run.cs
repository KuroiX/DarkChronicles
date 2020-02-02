using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float range = 3f;
    public float explosionCooldown = 10f;

    private Transform player;
    private Rigidbody2D rb;
    private Ghost ghost;
    
    
    private float remainingCD;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        ghost = animator.GetComponent<Ghost>();
    }

     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ghost.LookPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if(Mathf.Abs((player.position.x - rb.position.x)) < 20f && Mathf.Abs((player.position.y - rb.position.y)) < 22)
            rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= range)
        {
            animator.SetTrigger("Attack");
            player.position = new Vector3(40f,20f);
        }

        if (remainingCD <= 0)
        {
            animator.SetTrigger("Explode");
            ghost.Explode();
            remainingCD = explosionCooldown;
        }
        else
        {
            remainingCD -= Time.deltaTime;
        }

        if (ghost.lives <= 0)
        {
            ghost.GetBook();
            animator.SetTrigger("Death");
        }
        
    }
     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Explode");
    }
    
}
