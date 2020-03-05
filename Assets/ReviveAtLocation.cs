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
            StartCoroutine(Revive());
        }
    }

    IEnumerator Revive()
    {
        hero.GetComponent<Rigidbody2D>().gravityScale = 0f;
        hero.GetComponent<BoxCollider2D>().enabled = false;
        hero.GetComponent<Movement>().enabled = false;
        hero.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
            heroCollision.gameObject.transform.position = revivePosition;
        hero.GetComponent<Rigidbody2D>().gravityScale = 1f;
        hero.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1);
        hero.GetComponent<Movement>().enabled = true;
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
