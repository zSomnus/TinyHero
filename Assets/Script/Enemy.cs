using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool playerInRange;
    protected bool onGround;

    protected Vector2 spawnPoint;
    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] protected Vector2 movingRange;
    [SerializeField] protected Vector2 rangeOffset;
    protected Rigidbody2D rb;
    [SerializeField] protected int damage;
    protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    protected Animator animator;
    [SerializeField] protected float deathDelay;

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
        if(hp == 0)
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

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(rangeOffset.x + transform.position.x, rangeOffset.y + transform.position.y), movingRange);
    }
}
