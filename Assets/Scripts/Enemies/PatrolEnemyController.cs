using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController: MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float patrolRange = 3f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Options")]
    [SerializeField] private bool startMovingRight = true;

    private Vector3 startPosition;
    private int direction;

    private void Start()
    {
        startPosition = transform.position;
        direction = startMovingRight ? 1 : -1;
        UpdateFacing();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
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

}
