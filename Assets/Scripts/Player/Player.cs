using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private LifesSystem lifesSystem;

    private float inputH;
    private float directionLastJump = 0;

    private bool canDoubleJump = false;
    private bool isAttacking = false;
    private bool hasFallOutTheWorld = false;

    [Header("Move System")]
    [SerializeField] private float speedMovement;

    [Header("Jump System")]
    [SerializeField] private Transform groundController;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector3 groundDetectionDimensions;
    [SerializeField] private LayerMask whatIsJumpable;

    [Header("WallSlideAndJump System")]
    [SerializeField] private Transform wallController;
    [SerializeField] private Vector3 wallDetectionDimensions;
    [SerializeField] private float slidingSpeed;
    [SerializeField] private float wallJumpForceX;
    [SerializeField] private float wallJumpForceY;

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
        lifesSystem = GetComponent<LifesSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        if (!hasFallOutTheWorld)
        {
            Heigthcontrol();
        }
        if (!WallSlide() && !isAttacking)
        {
            HorizontalMovement();
        }
        Jump();
        WallJump();
        Fall();
        StartCoroutine(ThrowAttack());
    }

    private void HorizontalMovement()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("wallsliding") && (AmIOnTheGround() || canDoubleJump))
        {
            canDoubleJump = AmIOnTheGround();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            anim.SetBool("falling", true);
        }
    }

    private bool WallSlide()
    {
        if (!AmIOnTheGround() && AmIOnTheWall() && inputH != 0)
        {
            canDoubleJump = false;
            anim.SetBool("wallsliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
            return true;
        }
        anim.SetBool("wallsliding", false);
        return false;
    }

    private void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("wallsliding"))
        {
            if (((transform.eulerAngles == Vector3.zero && inputH == -1) || (transform.eulerAngles == new Vector3(0, 180, 0) && inputH == 1)) 
                && directionLastJump != inputH)
            {
                directionLastJump = inputH;
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(new Vector2(wallJumpForceX * inputH, wallJumpForceY), ForceMode2D.Impulse);
                anim.SetTrigger("jump");
                anim.SetBool("falling", true);
            }
        }
    }

    private void Fall()
    {
        if (!AmIOnTheGround())
        {
            anim.SetBool("falling", true);
        }
        else
        {
            directionLastJump = 0;
            anim.SetBool("falling", false);
            hasFallOutTheWorld = false;
        }
    }

    private bool AmIOnTheGround()
    {
        return Physics2D.OverlapBox(groundController.position, groundDetectionDimensions, 0f, whatIsJumpable);
    }

    private bool AmIOnTheWall()
    {
        return Physics2D.OverlapBox(wallController.position, wallDetectionDimensions, 0f, whatIsJumpable);
    }

    private IEnumerator ThrowAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0f, 0f);
            anim.SetTrigger("attack");
            isAttacking = true;
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
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
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                StartCoroutine(enemy.ReceiveDamage(attackDamage));
            }
        }
    }

    private void Heigthcontrol()
    {
        if (transform.position.y < -10)
        {
            StartCoroutine(lifesSystem.ReceiveDamage(1000000));
            hasFallOutTheWorld = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.DrawWireCube(wallController.position, wallDetectionDimensions);
        Gizmos.DrawWireCube(groundController.position, groundDetectionDimensions);
    }
}
