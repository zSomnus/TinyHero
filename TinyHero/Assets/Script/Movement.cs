using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [Space]
    [Header("Player State")]
    public bool inAir;


    [Space]
    [Header("Combo")]
    public bool isAttackOne;
    public bool isAttackTwo;
    public bool isAttackThree;
    [SerializeField] int attackCount;

    [Space]
    [Header("Walk")]
    [SerializeField] bool canMove;
    [SerializeField] float walkSpeed;

    [Space]
    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float minJumpSpeed;
    [SerializeField] float maxFallSpeed;

    [Space]
    [Header("Slide")]
    [SerializeField] float slideSpeed;
    [SerializeField] bool isSliding;
    [SerializeField] float slideCd;
    float slideTimer;
    bool startCounting;

    [Space]
    [Header("Multiplier")]
    [SerializeField] float fallMultiplier;
    [SerializeField] float lowJumpMultiplier;

    [Space]
    [Header("Wall Slide")]
    [SerializeField] float wallSlideSpeed;

    [Space]
    [Header("Wall Jump")]
    [SerializeField] Vector2 wallJumpVector;

    [Header("Climb")]
    [SerializeField] Vector3 climbVector;

    Collision collision;
    BoxCollider2D collider;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collision = GetComponent<Collision>();
        collider = GetComponent<BoxCollider2D>();
        canMove = true;
        startCounting = false;
        slideTimer = slideCd;
    }

    private void FixedUpdate()
    {
        FlipSprite();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sliding
        if (isSliding)
        {
            canMove = false;
            transform.position += new Vector3(transform.localScale.x * slideSpeed * Time.deltaTime, 0f, 0f);
            rb.velocity = Vector2.zero;
        }

        // Move right
        if (canMove)
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
        }
        if (startCounting)
        {
            slideTimer += Time.deltaTime;
        }

        // Input Slide
        if ((Input.GetButtonDown("Slide") || Input.GetAxisRaw("Slide") > 0.1f) && slideTimer >= slideCd)
        {
            startCounting = false;
            //StartCoroutine("SlideMove");
            SlideStart();
        }

        // Jump

        Jump();

        if (collision.OnGround())
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("InAir", false);
        }
        else
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("InAir", true);
        }

        // Jump & fall
        if (rb.velocity.y > 0.5f)
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

        

        // Attack
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    attackCount++;
        //    //animator.SetBool("Attacking", true);
        //    //Attack();
        //}

        //// Attack combo
        //if (attackCount > 0)
        //{
        //    //canMove = false;
        //    Attack();
        //    attackCount = 0;
        //}
        SimulatePhysics();

        if (collision.OnWall() && !collision.OnGround())
        {
            Climb();
            //OnWallSlide();
        }
        else
        {
            animator.SetBool("OnWall", false);
            canMove = true;
        }
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
        if (canMove)
        {
            float walkInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(walkInput * walkSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (collision.OnGround())
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("InAir", false);
                //collision.OnWall(false);
                //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                Debug.Log("Jump()");
            }else if (collision.OnWall() && !collision.OnGround())
            {
                rb.velocity += new Vector2(-transform.localScale.x * wallJumpVector.x, 0f);
                //rb.velocity += new Vector2(0f, wallJumpVector.y);
                //rb.velocity += new Vector2(0f, )
                canMove = true;
                Debug.Log("Wall Jump");
            }
        }
    }

    void Climb()
    {
        if (collision.OnWall())
        {
            rb.gravityScale = 0f;

            if (Input.GetButtonDown("Slide") || Input.GetAxisRaw("Hold") > 0.1f)
            {
                rb.velocity = new Vector2(0f, Input.GetAxis("Vertical"));
                //transform.position += new Vector3(0f, Input.GetAxis("Vertical") * 5f, 0f);
            }
            else
            {
                OnWallSlide();
                rb.gravityScale = 1f;
                Debug.Log("On wall slide");
            }
        }
    }

    //IEnumerator SlideMove()
    //{
    //    Debug.Log("Slide used");

    //    SlideStart();
    //    yield return new WaitForSeconds(slideCd);

    //    //canMove = true;
    //    //isSliding = false;
    //    //Debug.Log("SlideEnd()");
    //    //animator.SetBool("Sliding", false);
    //    //collider.size = collision.idleColSize;
    //    //collider.offset = collision.idleColOffset;
    //    //rb.gravityScale = 1f;

    //}

    void OnWallSlide()
    {
        animator.SetBool("OnWall", true);

        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        canMove = false;
    }

    void SlideStart()
    {
        //StartCoroutine(SlideMove(slideCd));
        if (collision.OnGround())
        {
            isSliding = true;
            Debug.Log("SlideStart()");
            animator.SetBool("Sliding", true);
            collider.size = collision.slideColSize;
            collider.offset = collision.slideColOffset;
            rb.gravityScale = 0f;
        }

        //transform.position += new Vector3(transform.localScale.x * slideSpeed * Time.deltaTime, 0f, 0f);
    }

    void SlideEnd()
    {
        canMove = true;
        isSliding = false;
        Debug.Log("SlideEnd()");
        animator.SetBool("Sliding", false);
        collider.size = collision.idleColSize;
        collider.offset = collision.idleColOffset;
        rb.gravityScale = 1f;
        //StartCoroutine("SlideStart");
        slideTimer = 0f;
        startCounting = true;
    }

    //void Attack()
    //{
    //    if (!isAttackOne)
    //    {
    //        animator.SetTrigger("Attack");
    //        animator.SetBool("Attack1", true);
    //        //canMove = false;

    //    }
    //}

    void AttackOneStart()
    {
        isAttackOne = true;
    }

    void AttackOneEnd()
    {
        isAttackOne = false;
        animator.SetBool("Attack1", false);
        //canMove = true;
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
