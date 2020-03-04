using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveAtLocation : MonoBehaviour
{
    PolygonCollider2D collider;
    [SerializeField] int spikeDamage;
    [SerializeField] bool hasPlayer;
    [SerializeField] GameObject reviveRoom;
    Vector2 revivePosition;
    HeroCollision heroCollision;
    Hero hero;

    

    public int SpikeDamage { get => spikeDamage; set => spikeDamage = value; }

    // Start is called before the first frame update
    void Start()
    {
        revivePosition = reviveRoom.transform.position;
        heroCollision = GameObject.Find("Hero").GetComponent<HeroCollision>();
        collider = GetComponent<PolygonCollider2D>();
        hero = GameObject.Find("Hero").GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCollision.OnSpike && hasPlayer)
        {
            heroCollision.OnSpike = false;
            hero.TakeDamage(spikeDamage);
            heroCollision.gameObject.transform.position = revivePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.layer == 9)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.layer == 9)
        {
            hasPlayer = false;
        }
    }
}
