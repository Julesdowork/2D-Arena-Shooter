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

    bool facingRight = true;
    bool isGrounded;

    Animator animator;
    Rigidbody2D rb2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
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
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetBool(TagManager.IS_GROUNDED_PARAM, isGrounded);
    }
}
