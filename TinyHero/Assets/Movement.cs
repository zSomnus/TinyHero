using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float walkSpeed;
    [Space]
    [SerializeField] float jumpSpeed;
    [SerializeField] float minJumpSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fallMultiplier;
    [SerializeField] float lowJumpMultiplier;



    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        Jump();

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

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
