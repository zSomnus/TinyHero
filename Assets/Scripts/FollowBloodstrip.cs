using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBloodstrip : MonoBehaviour
{

    [SerializeField] Transform target;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 pos = mainCamera.WorldToScreenPoint(target.position);
            transform.position = pos;
        }
    }
}
