﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float offsetFactor = 1000.0f;
    Material spriteMat;

    [SerializeField] bool autoMove = false;
    [SerializeField] float autoMoveAmount = 0.0f;

    float Offset;

    private void Awake()
    {
        spriteMat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (!autoMove)
        {
            Vector2 offset = transform.position;
            offset.y = 0;
            spriteMat.mainTextureOffset = offset / offsetFactor;
        }
        else
        {

            Offset += autoMoveAmount * Time.deltaTime;
            spriteMat.mainTextureOffset = new Vector2(Offset, 0);
        }
    }
}
