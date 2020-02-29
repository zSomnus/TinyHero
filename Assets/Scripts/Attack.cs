using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Animator animator;
    Collision collision;

    bool canAttack;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    Vector2 boxSize;

    LayerMask playerLayerMask;

    [SerializeField] private float attackCD;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collision = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                MeleeAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void MeleeAttack()
    {
        if (!collision.OnWall)
        {
            animator.SetTrigger("Attack");
            //animator.SetBool("Attack1", true);
            //canMove = false;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                //Debug.Log("We hit " + enemy.name);
                if(enemy != null)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
        else
        {
            Debug.Log("On wall attack");
        }
    }



    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
