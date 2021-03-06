﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollision : MonoBehaviour
{
    BoxCollider2D collider;

    [SerializeField] bool onGround;
    [SerializeField] bool onWall;
    [SerializeField] bool onWallCorner;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask spikeLayer;

    [Space]
    [SerializeField] Vector2 bottomOffset, rightOffset, leftOffset, rightCornerOffset, leftCornerOffset;
    [SerializeField] float collisionRadius = 0.1f;
    [SerializeField] Vector2 boxSizeWall;
    [SerializeField] Vector2 boxSizeGround;
    [Header("Slide")]
    public Vector2 slideColSize;
    public Vector2 slideColOffset;
    [Header("Idle")]
    public Vector2 idleColSize;
    public Vector2 idleColOffset;
    [Header("Climb")]
    public Vector2 climbColSize;
    public Vector2 climbColOffset;
    [Header("Jump")]
    public Vector2 jumpColSize;
    public Vector2 jumpColOffset;
    [Header("Fall")]
    public Vector2 fallColSize;
    public Vector2 fallColOffset;

    [Space]
    Hero hero;
    [SerializeField] bool onSpike;

    Animator animator;

    public bool OnWall { get => onWall; set => onWall = value; }
    public bool OnGround { get => onGround; set => onGround = value; }
    public bool OnWallCorner { get => onWallCorner; set => onWallCorner = value; }
    public bool OnSpike { get => onSpike; set => onSpike = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        hero = GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        //onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSizeGround, 0f, groundLayer);

        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, boxSizeWall, 0f, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, boxSizeWall, 0f, groundLayer);

        onWallCorner = Physics2D.OverlapBox((Vector2)transform.position + rightCornerOffset, boxSizeWall, 0f, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftCornerOffset, boxSizeWall, 0f, groundLayer);

        
    }

    private void FixedUpdate()
    {
        if (collider.IsTouchingLayers(spikeLayer))
        {
            onSpike = true;
        }
        else
        {
            onSpike = false;
        }
    }

    //void HitSpike()
    //{
    //    if (collider.IsTouchingLayers(spikeLayer))
    //    {
    //        Debug.Log("Ah!");
    //        OnSpike = true;
    //        hero.TakeDamage();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GetComponent<Hero>().TakeDamage collision.gameObject.GetComponent<ReviveAtLocation>().SpikeDamage;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset, rightCornerOffset, leftCornerOffset };

        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        //Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        //Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);


        Vector3 center = transform.position;
        Gizmos.DrawWireCube(new Vector3(center.x + bottomOffset.x, center.y + bottomOffset.y, center.z), boxSizeGround);
        Gizmos.DrawWireCube(new Vector3(center.x + rightOffset.x, center.y + rightOffset.y, center.z), boxSizeWall);
        Gizmos.DrawWireCube(new Vector3(center.x + leftOffset.x, center.y + leftOffset.y, center.z), boxSizeWall);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(center.x + rightCornerOffset.x, center.y + rightCornerOffset.y, center.z), boxSizeWall);
        Gizmos.DrawWireCube(new Vector3(center.x + leftCornerOffset.x, center.y + leftCornerOffset.y, center.z), boxSizeWall);



        //// Idle Collision Debug
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(idleColOffset, idleColSize);

        //// Slide Collision Debug
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(slideColOffset, slideColSize);

    }
}
