using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private LifeContrroller lifeController;
  
    [SerializeField] private LayerMask damageLayers;

    private void OnTriggerEnter(Collider other)
    {
        if ((damageLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        DamageSourceController source = other.GetComponent<DamageSourceController>();
        if (source == null)
            return;
        lifeController.GetDamage(source.Damage);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((damageLayers.value & (1 << collision.gameObject.layer)) == 0)
            return;

        DamageSourceController source = collision.gameObject.GetComponent<DamageSourceController>();
        if (source == null)
            return;
        lifeController.GetDamage(source.Damage);
    }
  
}
