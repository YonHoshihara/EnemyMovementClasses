using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private LifeContrroller lifeController;
    [SerializeField] private LayerMask damageLayers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        TryApplyDamage(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryApplyDamage(collision.gameObject);
    }

    private void TryApplyDamage(GameObject other)
    {
   
        if (other.transform != other.transform.root)
            return;

        if (other.transform.root == transform.root)
            return;

        if ((damageLayers.value & (1 << other.layer)) == 0)
            return;

        DamageSourceController source = other.GetComponent<DamageSourceController>();
        if (source == null)
            return;

        lifeController.GetDamage(source.Damage);
    }
}
