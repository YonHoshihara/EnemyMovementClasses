using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        rb.velocity = direction * speed;

        FaceDirection();
    }

    private void FaceDirection()
    {
        if (direction.x == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    
    public void OnDeath()
    {
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);
    }
}
