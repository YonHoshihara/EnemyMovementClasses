using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = Vector2.right;
        rb.velocity *= speed;
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
