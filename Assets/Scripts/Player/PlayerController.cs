using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;

    private float horizontal;
    private bool grounded;

    // timers
    private float coyoteCounter;
    private float jumpBufferCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadInput();
        UpdateJumpBuffer();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
        HandleJump();
    }

    // ---------------- INPUT ----------------
    private void ReadInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
    }

    private void UpdateJumpBuffer()
    {
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;
    }

    // ---------------- MOVEMENT ----------------
    private void Move()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", horizontal);
        // Flip visual
        if (horizontal != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(horizontal) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    // ---------------- JUMP ----------------
    private void HandleJump()
    {
        // atualiza coyote time
        if (grounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.fixedDeltaTime;

        // executa o pulo
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }

    // ---------------- GROUND ----------------
    private void CheckGround()
    {
        grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
        animator.SetBool("IsGrounded", grounded);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
#endif
}
