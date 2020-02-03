﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [Space]
    public bool isAttackOne;
    public bool isAttackTwo;
    public bool isAttackThree;

    [SerializeField] float walkSpeed;
    [Space]
    [SerializeField] float jumpSpeed;
    [SerializeField] float minJumpSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fallMultiplier;
    [SerializeField] float lowJumpMultiplier;

    Collision collision;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collision = GetComponent<Collision>();
    }

    private void FixedUpdate()
    {
        FlipSprite();
        
    }

    // Update is called once per frame
    void Update()
    {


        Walk();
        if(Input.GetAxis("Horizontal") != 0f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (collision.OnGround())
        {
            animator.SetBool("Jumping", false);
            Jump();
        }
        else
        {
            animator.SetBool("Jumping", true);
        }

        if(rb.velocity.y > 0.5f)
        {
            animator.SetBool("Jumping", true);
        }
        else if(rb.velocity.y < -1f)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attacking", true);
            Attack();
        }

        SimulatePhysics();
    }

    void SimulatePhysics()
    {
        // simulate physics
        if (rb.velocity.y < minJumpSpeed && rb.velocity.y > -maxFallSpeed)
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Walk()
    {
        float walkInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(walkInput * walkSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            Debug.Log("Jump()");
        }
    }

    void Attack()
    {
        if (!isAttackOne)
        {
            isAttackOne = true;
            animator.SetBool("Attack1", true);
        }
        else
        {
            if (!isAttackTwo)
            {
                isAttackTwo = true;
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", true);
            }
            else
            {
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", true);
            }
        }
    }

    void AttackOneStart()
    {
        isAttackOne = true;
    }

    void AttackOneEnd()
    {
        isAttackOne = false;
    }

    void AttackTwoStart()
    {
        isAttackTwo = true;
    }

    void AttackTwoEnd()
    {
        isAttackTwo = false;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon + 0.5f;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}