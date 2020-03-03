using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected BoxCollider2D collider;

    protected bool playerInMoveRange;
    protected bool playerInAttackRange;
    protected bool onGround;
    protected bool hitWall;

    protected bool isAttacking;

    protected Vector2 spawnPoint;

    [Header("Layers")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask groundLayer;

    [Header("Ranges")]
    [SerializeField] protected Vector2 movingRange;
    [SerializeField] protected Vector2 rangeOffset;
    [SerializeField] protected Vector2 hitWallRange;
    [SerializeField] protected Vector2 attackRange;
    [SerializeField] protected Vector2 attackOffset;

    [Header("Enemy Status")]
    [SerializeField] protected int maxHp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float deathDelay;

    protected Rigidbody2D rb;
    protected int hp;
    protected Animator animator;

    protected GameObject hero;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        animator = GetComponent<Animator>();
        spawnPoint = rb.transform.position;
        hero = GameObject.Find("Hero");
        collider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        FlipSprite();
        playerInMoveRange = Physics2D.OverlapBox((Vector2)transform.position + rangeOffset, movingRange, 0f, playerLayer);
        hitWall = Physics2D.OverlapBox((Vector2)transform.position, hitWallRange, 0f, groundLayer);
        playerInAttackRange = Physics2D.OverlapBox((Vector2)transform.position + attackOffset, attackRange, 0f, playerLayer);

        if (playerInAttackRange)
        {
            MeleeAttack();
        }
        CatchPlayer();
    }


    internal virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp == 0)
        {
            Die(deathDelay);
        }
    }

    void Die(float delay)
    {
        animator.SetTrigger("Die");
        Debug.Log("(X_X)");
        Destroy(this.gameObject, delay);
    }
    protected void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon + 0.5f;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    protected virtual void MeleeAttack()
    {
        animator.SetTrigger("Attack");
    }

    protected virtual void ApplyDamage()
    {
        Debug.Log("T_T");
        hero.GetComponent<Hero>().TakeDamage(damage);
    }

    protected void CatchPlayer()
    {
        if (playerInMoveRange && hp > 0 && !playerInAttackRange)
        {
            if (isAttacking)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                Debug.Log("Moving");
                if (hero.transform.position.x - transform.position.x > collider.size.x / 2f)    //transform.position.x + this.collider.size.x / 2f
                {
                    Debug.Log("Slime Move Right");
                    animator.SetBool("Move", true);
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else if (hero.transform.position.x - transform.position.x < -collider.size.x / 2f)
                {
                    Debug.Log("Slime Move Left");
                    animator.SetBool("Move", true);
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    Debug.Log("Slime Stoped");
                    animator.SetBool("Move", false);
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

            }
        }
        else
        {
            Debug.Log("Slime Stoped");
            animator.SetBool("Move", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (hitWall)
        {
            animator.SetBool("Move", false);
        }
    }

    protected void CheckAttack(int n)
    {
        if (n == 1)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(rangeOffset.x + transform.position.x, rangeOffset.y + transform.position.y), movingRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), hitWallRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + attackOffset.x, transform.position.y + attackOffset.y), attackRange);
    }
}
