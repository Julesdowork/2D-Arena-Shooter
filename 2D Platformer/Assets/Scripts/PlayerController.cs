using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 40f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 1f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float fallBoundary = -9f;

    bool facingRight = true;
    bool isGrounded;

    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    PlayerStats playerStats;

    public bool FacingRight {
        get { return facingRight; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= fallBoundary)
            playerStats.DamagePlayer(Mathf.Infinity);

        CheckIfGrounded();
        Move();
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw(TagManager.HORIZONTAL);
        rb2D.velocity = new Vector2(xMove * speed, rb2D.velocity.y);
        animator.SetFloat(TagManager.SPEED_PARAM, Mathf.Abs(xMove));

        if (!facingRight && xMove > 0)
            Flip();
        else if (facingRight && xMove < 0)
            Flip();
    }

    void Jump()
    {
        if (Input.GetButton(TagManager.JUMP) && isGrounded)
        {
            rb2D.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;    // by default flipX is false and facingRight is true
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetBool(TagManager.IS_GROUNDED_PARAM, isGrounded);
    }
}
