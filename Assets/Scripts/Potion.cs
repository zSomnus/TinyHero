using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount;
    protected GameObject hero;


    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hero.GetComponent<Hero>().Healing(healAmount);
            Destroy(gameObject);
        }
    }
}
