using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float travelSpeed = 0.175f;
    private float lifeTime = 5.0f;
    private AudioSource audioSource;
    [SerializeField] AudioClip projectileSFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            ProjectileSFX();
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void ChangeDirection()
    {
        travelSpeed = -travelSpeed;
    }

    private void ProjectileSFX()
    {
        audioSource.PlayOneShot(projectileSFX);
    }
}
