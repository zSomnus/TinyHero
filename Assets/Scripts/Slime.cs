using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    int hitPoint;
    int maxHitPoint;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        maxHitPoint = 3;
        hitPoint = maxHitPoint;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("HP", hitPoint);
    }
}
