using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [Range(2f, 100f)]
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HealthScript>().TakeDamage(damage);
        }
        else if (LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == LayerMask.GetMask("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
