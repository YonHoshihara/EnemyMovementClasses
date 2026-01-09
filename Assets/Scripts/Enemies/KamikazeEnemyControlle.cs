using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemyControlle : MonoBehaviour
{
    [SerializeField] private float pursuitDistance = 6f;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float offscreenMargin = 1.5f;

    [SerializeField] private Animator animator;
    private Transform player;

    private Rigidbody2D rb;

    private bool activated;
    private bool canMove = true;
    private float direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!canMove) return;
        if (!activated)
        {
            CheckPlayerDistance();
            return;
        }

        Move();
        CheckOffscreen();
    }

    private void CheckPlayerDistance()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX <= pursuitDistance)
        {
            activated = true;
            direction = Mathf.Sign(player.position.x - transform.position.x);
            Flip();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, 0f);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    private void CheckOffscreen()
    {
        if (!IsVisibleFromCamera())
        {
            OnDeath();
        }
    }

    private bool IsVisibleFromCamera()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        return viewportPos.x > -offscreenMargin &&
               viewportPos.x < 1 + offscreenMargin;
    }

    public void OnDeath(){

        canMove = false;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        Destroy(gameObject,1f);
    }
}
