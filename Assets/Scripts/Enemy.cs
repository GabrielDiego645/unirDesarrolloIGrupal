using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Patrol System")]
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected float patrolSpeed;

    [Header("Attack System")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected LayerMask whatIsDamageable;

    [Header("Lifes System")]
    [SerializeField] protected float health;
    [SerializeField] protected Slider healthBar;

    protected Vector3 currentTarget;
    protected int currentIndex = 0;
    protected Animator anim;
    protected GameObject player;
    protected bool playerDetected = false;
    protected bool hasRecibedDamage = false;

    protected void DefineNewTarget()
    {
        currentIndex++;
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }
        currentTarget = waypoints[currentIndex].position;
        FocusTarget();
    }

    protected void FocusTarget()
    {
        if (currentTarget.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Runs from animation event
    protected void Attack()
    {
        Collider2D[] collidersTouched = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D collider in collidersTouched)
        {
            if (collider.gameObject.CompareTag("PlayerHitBox"))
            {
                LifesSystem lifesSystem = collider.gameObject.GetComponent<LifesSystem>();
                StartCoroutine(lifesSystem.ReceiveDamage(attackDamage));
            }
        }
    }

    protected void FocusPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public IEnumerator ReceiveDamage(float damageRecibed)
    {
        hasRecibedDamage = true;
        Debug.Log("Actor damaged");
        health -= damageRecibed;
        healthBar.value = health;
        if (health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("death");
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            player = collision.gameObject;
            playerDetected = true;
        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collision.gameObject.GetComponent<LifesSystem>();
            StartCoroutine(lifesSystem.ReceiveDamage(attackDamage));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            playerDetected = false;
        }
    }
}
