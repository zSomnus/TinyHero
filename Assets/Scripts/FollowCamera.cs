using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraBackground
{
    None = -1,
    Cave
}

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds;

    private void Awake()
    {
        
    }
    void Update()
    {
    }
}
