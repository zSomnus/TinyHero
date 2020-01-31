using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Movement movement;

    [SerializeField] Vector2 boxSize;

    [SerializeField] private bool cameraMove;

    [SerializeField] private float verticalDis; // Player vertical distance to the boundary
    [SerializeField] private float horizontalDis = 0; // Player horizontal distance to the boundary
    // Start is called before the first frame update
    void Start()
    {
        movement = FindObjectOfType<Movement>();
    }

    private void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        CheckBoundary();
        if (cameraMove)
        {
            FollowPlayer();
        }

        //FollowPlayer();
        
    }

    private void FollowPlayer()
    {
        Vector2 newCamPos = new Vector2(movement.transform.position.x, movement.transform.position.y);
        Vector3 targetPos = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.08f);
        if (verticalDis < (boxSize.y * 0.5f) || horizontalDis > (boxSize.x * 0.5f))
        {
            cameraMove = false;
        }
    }

    private void CheckBoundary()
    {
        if (movement.transform.position.y != transform.position.y)
        {
            verticalDis = Mathf.Abs(transform.position.y - movement.transform.position.y);
        }
        if (verticalDis > (boxSize.y * 0.5f))// || verticalDis > boxSize.y * 0.5f)
        {
            cameraMove = true;
        }

        if (movement.transform.position.x != transform.position.x)
        {
            horizontalDis = Mathf.Abs(movement.transform.position.x - transform.position.x);
        }

        if (horizontalDis > boxSize.x * 0.5f) // if player is out of the left boundary
        {
            cameraMove = true; // camera start tracking player position
        }
    }

    private void OnDrawGizmos()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float boxYOffset = boxSize.y * 0.5f;
        float boxXOffset = boxSize.x * 0.5f;
        Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(transform.position, boxSize);
        Gizmos.DrawLine(new Vector3(x - boxXOffset, y + boxYOffset, z), new Vector3(x + boxXOffset, y + boxYOffset, z));
        Gizmos.DrawLine(new Vector3(x - boxXOffset, y - boxYOffset, z), new Vector3(x + boxXOffset, y - boxYOffset, z));
        Gizmos.DrawLine(new Vector3(x + boxXOffset, y + boxYOffset, z), new Vector3(x + boxXOffset, y - boxYOffset, z));
        Gizmos.DrawLine(new Vector3(x - boxXOffset, y + boxYOffset, z), new Vector3(x - boxXOffset, y - boxYOffset, z));
    }
}
