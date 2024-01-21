using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;
    private float lastY;

    [Header("Move System")]
    [SerializeField] private Transform pies;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float speedMovement;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDetectionDistance;
    [SerializeField] private LayerMask whatIsJumpable;

    [Header("Attack System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackDamage;
    [SerializeField] private LayerMask whatIsDamageable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
        Fall();
        ThrowAttack();
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("hurt");
        }
    }

    private void HorizontalMovement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * speedMovement, rb.velocity.y);

        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AmIOnTheGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            anim.SetBool("falling", true);
        }
    }

    private void Fall()
    {
        if (lastY > transform.position.y && !AmIOnTheGround())
        {
            anim.SetBool("falling", true);
        } 
        else if (anim.GetBool("falling") && AmIOnTheGround())
        {
            anim.SetBool("falling", false);
        }
        lastY = transform.position.y;
    }

    private bool AmIOnTheGround()
    {
        return Physics2D.Raycast(pies.position, Vector3.down, groundDetectionDistance, whatIsJumpable);
        //    || Physics2D.Raycast(pies.position, Vector3.left, 1f, whatIsJumpable)
        //    || Physics2D.Raycast(pies.position, Vector3.right, 1f, whatIsJumpable);
    }

    private void ThrowAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    // Runs from animation event
    private void Attack()
    {
        Collider2D[] collidersTouched = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D collider in collidersTouched)
        {
            if (!collider.gameObject.CompareTag("PlayerHitBox"))
            {
                LifesSystem lifesSystem = collider.gameObject.GetComponent<LifesSystem>();
                lifesSystem.ReceiveDamage(attackDamage);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }*/
}
