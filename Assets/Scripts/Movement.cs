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
    [Header("Run")]
    [SerializeField] bool canMove;
    [SerializeField] bool isMoving;
    [SerializeField] float runSpeed;

    [Space]
    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float minJumpSpeed;
    [SerializeField] float maxFallSpeed;

    [Space]
    [Header("Double Jump")]
    [SerializeField] bool canDoubleJump;

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
    [SerializeField] Vector2 cornerForce;
    [SerializeField] bool isHolding;

    HeroCollision heroCollision;
    BoxCollider2D collider;
    Animator animator;

    public bool cornerClimbing;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        heroCollision = GetComponent<HeroCollision>();
        collider = GetComponent<BoxCollider2D>();
        canMove = true;
        startCounting = false;
        slideTimer = slideCd;
        isMoving = false;
    }

    private void FixedUpdate()
    {
        FlipSprite();
        ClimbOnCorner();

        // Input Slide
        if ((Input.GetButtonDown("Slide") || Input.GetAxisRaw("Slide") > 0.1f) && slideTimer >= slideCd)
        {
            startCounting = false;
            //StartCoroutine("SlideMove");
            SlideStart();
        }

        // Sliding
        if (isSliding)
        {
            canMove = false;
            if (!heroCollision.OnWall)
            {
                transform.position += new Vector3(transform.localScale.x * slideSpeed * Time.deltaTime, 0f, 0f);
                rb.velocity = Vector2.zero;

            }
        }


        if (Input.GetButtonDown("Hold") || Input.GetAxisRaw("Hold") > 0.1f)
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

        // Move right
        if (canMove)
        {
            Run();
            if (Input.GetAxis("Horizontal") != 0f)
            {
                animator.SetBool("Moving", true);
                isMoving = true;
            }
            else
            {
                animator.SetBool("Moving", false);
                isMoving = false;
            }
        }
        if (startCounting)
        {
            slideTimer += Time.deltaTime;
        }

        //// Input Slide
        //if ((Input.GetButtonDown("Slide") || Input.GetAxisRaw("Slide") > 0.1f) && slideTimer >= slideCd)
        //{
        //    startCounting = false;
        //    //StartCoroutine("SlideMove");
        //    SlideStart();
        //}

        // Jump

        Jump();
        DoubleJump();

        if (heroCollision.OnGround)
        {
            animator.SetBool("InAir", false);
            canDoubleJump = true;
        }
        else
        {
            animator.SetBool("InAir", true);
        }

        if (heroCollision.OnGround && !heroCollision.OnWall)
        {
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Jumping", true);
        }

        // Jump & fall
        if (rb.velocity.y > 0.5f && !heroCollision.OnGround)
        {
            animator.SetBool("Jumping", true);
        }
        else if (rb.velocity.y < -0.5f && !heroCollision.OnGround)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else// if(collision.OnGround)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }

        if (heroCollision.OnWall)
        {
            canDoubleJump = true;
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


        Climb();

        animator.SetFloat("VerticalVelocity", rb.velocity.y);


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

    void Run()
    {
        if (canMove)
        {
            float runInput = Input.GetAxis("Horizontal");
            if (runInput > 0)
            {
                runInput = runSpeed;
            }
            else if (runInput < 0)
            {
                runInput = -runSpeed;
            }
            else
            {
                runInput = 0f;
            }
            rb.velocity = new Vector2(runInput, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && heroCollision.OnGround)
        {
            animator.SetBool("Jumping", false);
            //collision.OnWall(false);
            //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            collider.size = new Vector2(0.82f, 1.4f);
            collider.offset = new Vector2(0.21f, 0.13f);
            //else if (collision.OnWall && !collision.OnGround)
            //{
            //    rb.velocity += new Vector2(-transform.localScale.x * wallJumpVector.x, wallJumpVector.y);
            //    //rb.velocity += new Vector2(0f, wallJumpVector.y);
            //    //rb.velocity += new Vector2(0f, )
            //    canMove = true;
            //    Debug.Log("Wall Jump");
            //}
            //else
            //{

            //}
        }
    }

    void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") && !heroCollision.OnGround && !heroCollision.OnWall && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            canDoubleJump = false;
            animator.SetTrigger("DoubleJump");
        }
    }

    void Climb()
    {
        if (heroCollision.OnWall)// && !collision.OnGround)
        {
            collider.size = heroCollision.climbColSize;
            collider.offset = heroCollision.climbColOffset;

            if (isHolding)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(0f, Input.GetAxis("Vertical") * climbVector.y);
                animator.SetBool("Hold", true);
                //transform.position += new Vector3(0f, Input.GetAxis("Vertical") * 5f, 0f);
            }
            else
            {
                animator.SetBool("Hold", false);
                canMove = true;
                rb.gravityScale = 1f;
                if ((transform.localScale.x == 1 && Input.GetAxis("Horizontal") > 0.2f) ||
                    (transform.localScale.x == -1 && Input.GetAxis("Horizontal") < -0.2f))
                {
                    //animator.SetBool("Hold", false);
                    //OnWallSlide();
                    if (rb.velocity.y < 0f)
                    {
                        OnWallSlide();
                        canDoubleJump = true;
                    }

                }
                else
                {
                    animator.SetBool("OnWall", false);
                }
            }
            //if (Input.GetButtonUp("Slide"))
            //{
            //    animator.SetBool("Hold", false);

            //}
        }
        else if (heroCollision.OnWallCorner)
        {
            rb.gravityScale = 0f;
            //canMove = false;
        }
        else
        {
            rb.gravityScale = 1f;
            animator.SetBool("OnWall", false);
            animator.SetBool("Hold", false);
            canMove = true;
            collider.size = heroCollision.idleColSize;
            collider.offset = heroCollision.idleColOffset;
        }
    }

    void ClimbOnCorner()
    {
        //if (collision.OnGround == false && collision.OnWall == false && collision.OnWallCorner == true)
        if (heroCollision.OnWallCorner && !heroCollision.OnWall && !heroCollision.OnGround)
        {
            //if (!collision.OnGround && !collision.OnWall && !collision.OnWallCorner)
            //{
            //collider.size = new Vector2(collision.climbColSize.x, 0.8f);
            //collider.offset = new Vector2(collision.climbColOffset.x, 0.25f);
            //cornerClimbing = true;
            rb.gravityScale = 0f;
            animator.SetBool("CornerClimb", true);
            //transform.position += new Vector3(transform.localScale.x * 0.5f, 1f, 0);
            //}
        }
        else
        {
            //cornerClimbing = false;
            animator.SetBool("CornerClimb", false);
        }

        if (cornerClimbing)
        {
            rb.velocity = Vector2.zero;
            transform.position += new Vector3(0, cornerForce.y, 0);
            if (heroCollision.OnWallCorner == false)
            {
                transform.position += new Vector3(transform.localScale.x * cornerForce.x, 0, 0);
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
    void CornerClimbStart()
    {
        //collider.size = collision.climbColSize;
        //collider.offset = collision.climbColOffset;
        cornerClimbing = true;
        canMove = false;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }
    void CornerClimbEnd()
    {
        //collider.size = collision.idleColSize;
        //collider.offset = collision.idleColOffset;
        cornerClimbing = false;
        canMove = true;
        rb.gravityScale = 1f;
    }

    void OnWallSlide()
    {
        animator.SetBool("OnWall", true);

        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        canMove = false;
    }

    void SlideStart()
    {
        //StartCoroutine(SlideMove(slideCd));
        if (heroCollision.OnGround)
        {
            Debug.Log("Slide");
            isSliding = true;
            animator.SetBool("Sliding", true);
            collider.size = heroCollision.slideColSize;
            collider.offset = heroCollision.slideColOffset;
            rb.gravityScale = 0f;
        }

        //transform.position += new Vector3(transform.localScale.x * slideSpeed * Time.deltaTime, 0f, 0f);
    }

    void SlideEnd()
    {
        canMove = true;
        isSliding = false;
        animator.SetBool("Sliding", false);
        collider.size = heroCollision.idleColSize;
        collider.offset = heroCollision.idleColOffset;
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
        if (playerHasHorizontalSpeed && isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
