﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        maxHp = 1;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("HP", base.hp);
    }
}
