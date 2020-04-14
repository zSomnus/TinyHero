using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float travelSpeed = 0.175f;
    private float lifeTime = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + travelSpeed, transform.position.y);


        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void ChangeDirection()
    {
        travelSpeed = -travelSpeed;
    }
}
