using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    BoxCollider2D collider;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        maxHp = 1;
        collider = GetComponent<BoxCollider2D>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        animator.SetInteger("HP", base.hp);
        //FlipSprite();
        //playerInMoveRange = Physics2D.OverlapBox((Vector2)transform.position + rangeOffset, movingRange, 0f, playerLayer);
        //hitWall = Physics2D.OverlapBox((Vector2)transform.position, hitWallRange, 0f, groundLayer);
        //playerInAttackRange = Physics2D.OverlapBox((Vector2)transform.position + attackOffset, attackRange, 0f, playerLayer);

        //if (playerInAttackRange)
        //{
        //    MeleeAttack();
        //}
        base.Update();
    }


    //void CatchPlayer()
    //{
    //    if (playerInMoveRange && hp > 0 && !playerInAttackRange && !isAttacking)
    //    {
    //        Debug.Log("Moving");
    //        if (hero.transform.position.x - transform.position.x > collider.size.x / 2f)    //transform.position.x + this.collider.size.x / 2f
    //        {
    //            Debug.Log("Slime Move Right");
    //            animator.SetBool("Move", true);
    //            rb.velocity = new Vector2(speed, rb.velocity.y);
    //        }
    //        else if (hero.transform.position.x - transform.position.x < -collider.size.x / 2f)
    //        {
    //            Debug.Log("Slime Move Left");
    //            animator.SetBool("Move", true);
    //            rb.velocity = new Vector2(-speed, rb.velocity.y);
    //        }
    //        else
    //        {
    //            Debug.Log("Slime Stoped");
    //            animator.SetBool("Move", false);
    //            rb.velocity = Vector2.zero;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Slime Stoped");
    //        animator.SetBool("Move", false);
    //        rb.velocity = Vector2.zero;
    //    }

    //    if (hitWall)
    //    {
    //        animator.SetBool("Move", false);
    //    }
    //}

    protected override void ApplyDamage()
    {
        if (playerInAttackRange)
        {
            base.ApplyDamage();

        }
    }
}
