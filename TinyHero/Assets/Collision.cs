using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] bool onGround;
    [SerializeField] bool onWall;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;

    [Space]
    [SerializeField] Vector2 bottomOffset, rightOffset, leftOffset;
    [SerializeField] float collisionRadius = 0.1f;
    [SerializeField] Vector2 boxSize;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, boxSize, 0f, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, boxSize, 0f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        //Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        //Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

        Vector3 center = transform.position;
        Gizmos.DrawWireCube(new Vector3(center.x + rightOffset.x, center.y + rightOffset.y, center.z), boxSize);
        Gizmos.DrawWireCube(new Vector3(center.x + leftOffset.x, center.y + leftOffset.y, center.z), boxSize);
    }

    public bool OnGround()
    {
        return onGround;
    }

    public bool OnWall()
    {
        return onWall;
    }
}
