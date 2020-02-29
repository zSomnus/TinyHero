using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] Image hearts;
    [SerializeField] Sprite[] hitPointSprites;

    [SerializeField] int hp;
    int mana;
    [SerializeField] int maxHp;
    [SerializeField] int maxMana;

    [SerializeField] int meleeDamage;
    [SerializeField] int rangeDamage;

    [SerializeField] float stamina;

    public int HitPoint { get => hp; set => hp = value; }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        //hitPointSprites = new Sprite[6];
        hearts = GameObject.Find("Hearts").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (hp)
        {
            case 0:
                hearts.sprite = hitPointSprites[0];
                Debug.Log("HP: 0");
                break;
            case 1:
                hearts.sprite = hitPointSprites[1];
                Debug.Log("HP: 1");
                break;
            case 2:
                hearts.sprite = hitPointSprites[2];
                Debug.Log("HP: 2");
                break;
            case 3:
                hearts.sprite = hitPointSprites[3];
                Debug.Log("HP: 3");
                break;
            case 4:
                hearts.sprite = hitPointSprites[4];
                Debug.Log("HP: 4");
                break;
            case 5:
                hearts.sprite = hitPointSprites[5];
                Debug.Log("HP: 5");
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
