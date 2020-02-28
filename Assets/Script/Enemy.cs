using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb;
    [SerializeField] protected int damage;
    protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    protected Animator animator;
    [SerializeField] protected float deathDelay;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
