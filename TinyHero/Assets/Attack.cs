using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Animator animator;

    Vector2 boxSize;

    LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    MeleeAttack();
        //}
    }

    //void MeleeAttack()
    //{
    //    animator.SetTrigger("Attack");
    //    animator.SetBool("Attack1", true);
    //    //canMove = false;
    //}

    public void AttackFunc()
    {
        if (Input.GetKeyDown("Attack"))
        {
            //CheckAckInteractive(int dir);
        }

    }

    public void CheckAckInteractive(int dir)
    {
        float distance = 1.8f;
        RaycastHit2D hit2D = new RaycastHit2D();
        Vector2 raySize = new Vector2(boxSize.x + 0.5f, boxSize.y);

        switch (dir)
        {
            case 1:
                hit2D = Physics2D.BoxCast(transform.position, raySize, 0, Vector2.left, distance, playerLayerMask);
                break;
            case 2:
                hit2D = Physics2D.BoxCast(transform.position, raySize, 0, Vector2.left, distance, playerLayerMask);
                break;
        }
    }
}
