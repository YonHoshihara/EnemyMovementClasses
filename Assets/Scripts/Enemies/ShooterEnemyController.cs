using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : MonoBehaviour
{
    [SerializeField] private float attackDistance = 2.5f;
    [SerializeField] private float attackCooldown = 1.2f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject fireballSpawnPoint;


    private Transform player;
    private float nextAttackTime;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private int direction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX <= attackDistance)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time < nextAttackTime)
            return;
        FacePlayer();
        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + attackCooldown;
    }


    private void FacePlayer()
    {
        direction = player.position.x > transform.position.x ? 1 : -1;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public void SpawnFireball()
    {
       GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.transform.position,Quaternion.identity);
       FireBallController fireballController = fireball.GetComponent<FireBallController>();
       fireballController.SetDirection(new Vector2(direction,0));
    }

    public void OnDeath()
    {
        boxCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
    }
}
