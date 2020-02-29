using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
