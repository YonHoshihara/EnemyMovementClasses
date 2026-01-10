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
    
    [Header("Animator")]
    [SerializeField] private LifeContrroller lifeController;
    
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [SerializeField]
    private VoidEventSO onPlayerHitsEnemy;

    private Rigidbody2D rb;

    private float horizontal;
    private bool grounded;

    // timers
    private float coyoteCounter;
    private float jumpBufferCounter;

    // states
    private bool isAttacking;
    private bool isHurt;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead || isHurt) return;

        ReadInput();
        UpdateJumpBuffer();
        HandleAttack();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        CheckGround();

        if (isHurt)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetFloat("Speed", 0f);
            return;
        }

        HandleJump();
        Move();
    }

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
    private void Move()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetFloat("Speed", 0f);
            return;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (horizontal != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(horizontal) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void HandleJump()
    {
        if (isAttacking) return;

        if (grounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.fixedDeltaTime;

        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }

        animator.SetFloat("VerticalVelocity", rb.velocity.y);
    }

    private void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isHurt && !isDead)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void OnAttackAnimationFinished()
    {
        if (isDead) return;
        isAttacking = false;
    }

    private void CheckGround()
    {
        grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        animator.SetBool("IsGrounded", grounded);
    }

    public void OnHurt()
    {
        if (isDead || isHurt) return;

        isHurt = true;
        isAttacking = false;

        animator.SetBool("IsHurt", true);
        animator.SetTrigger("Hurt");
    }

    public void OnHurtAnimationFinished()
    {
        if (isDead) return;

        isHurt = false;
        animator.SetBool("IsHurt", false);
    }

    public void OnDeath()
    {
        if (isDead) return;

        isDead = true;
        isAttacking = false;
        isHurt = false;

        rb.velocity = Vector2.zero;

        animator.SetBool("IsHurt", false);
        animator.SetTrigger("Death");
    }
    public void OnPlayerHitsEnemyListener()
    {
        lifeController.RegenLife(1);
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
