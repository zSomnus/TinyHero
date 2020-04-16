using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount;
    protected GameObject hero;
    private AudioSource audioSource;
    [SerializeField] AudioClip healingSFX;

    void Start()
    {
        hero = GameObject.Find("Hero");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HealingSFX();
            hero.GetComponent<Hero>().Healing(healAmount);
            Destroy(gameObject);
        }
    }

    private void HealingSFX()
    {
        audioSource.PlayOneShot(healingSFX);
    }
}
