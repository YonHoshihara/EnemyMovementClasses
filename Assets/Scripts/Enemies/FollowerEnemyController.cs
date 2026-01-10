using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemyController : MonoBehaviour
{
    
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Animator animator;
    
    private Transform player;
    private Rigidbody2D rb;
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX <= chaseDistance)
        {
            Chase();
        }
        else
        {
            Stop();
        }
    }

    private void Chase()
    {
        animator.SetBool("Walk", true);
        int direction = player.position.x > transform.position.x ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        UpdateFacing(direction);
    }

    private void Stop()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        animator.SetBool("Walk",false);
    }

    private void UpdateFacing(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    public void OnDeath() { 
        canMove = false;
        Stop();
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }
}
