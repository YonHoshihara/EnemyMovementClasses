using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : MonoBehaviour
{
    [SerializeField] private float patrolRange = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool startMovingRight = true;
    [SerializeField] Animator animator;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private bool canMove = true;

    private Vector3 startPosition;
    private int direction;

    private void Start()
    {
        startPosition = transform.position;
        direction = startMovingRight ? 1 : -1;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        UpdateFacing();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (!canMove) return;

        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        float distanceFromStart = transform.position.x - startPosition.x;

        if (Mathf.Abs(distanceFromStart) >= patrolRange)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        direction *= -1;
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public void OnDeath()
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Static;
        boxCollider.enabled = false;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

    

}
