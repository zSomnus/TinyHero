using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float health;
    public int damage = 1;
    public Transform target;
    public float range;
    public float chaseSpeed;
    Rigidbody2D rb2d;
    protected Animator animator;
    protected GameObject hero;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hero = GameObject.Find("Hero");
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);
        if (distToPlayer < range)
        {
            Chase();
            
        }
        else
        {
            StopChase();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") 
        {
            animator.SetTrigger("Attack");
            hero.GetComponent<Hero>().TakeDamage(damage);
            Debug.Log("Damage Taken");
        }
    }

    void TakeDamage() 
    {
        health--;
    }

    void Chase() 
    {
        if (transform.position.x < target.position.x)
        {
            rb2d.velocity = new Vector2(chaseSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-chaseSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    void StopChase()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
