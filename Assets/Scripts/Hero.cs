using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    BoxCollider2D collider;
    [SerializeField] LayerMask spikeLayer;

    [SerializeField] Image hearts;
    [SerializeField] Sprite[] hitPointSprites;

    [SerializeField] float hp;
    int mana;
    [SerializeField] float maxHp;
    [SerializeField] float maxMana;

    [SerializeField] float meleeDamage;
    [SerializeField] float rangeDamage;

    [SerializeField] float stamina;


    public float HitPoint { get => hp; set => hp = value; }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        //hitPointSprites = new Sprite[6];
        //hearts = GameObject.Find("Hearts").GetComponent<Image>();
        hearts = GameObject.Find("HpFill").GetComponent<Image>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HP: " + HpPercentage(hp, maxHp));
        hearts.fillAmount = HpPercentage(hp, maxHp);
    }

    public float HpPercentage(float currentHp, float maxHp)
    {
        return currentHp / maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    public void Healing(int heal) 
    {
        hp += heal;
    }
}
