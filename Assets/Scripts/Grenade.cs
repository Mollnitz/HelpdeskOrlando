using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Range(2f, 100f)]
    public float damage = 10f;
    public float detonationTime = 5f;
    public float range = 2f;

    private Collider2D myCollider;

    private IEnumerator Start()
    {
        //Cache our collider
        myCollider = GetComponent<Collider2D>();
        if(myCollider.isTrigger) Debug.LogWarning("Grenade collider set to \"Is Trigger\"");

        //Start the countdown
        yield return new WaitForSeconds(detonationTime);

        //Detonate it
        Detonate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Will only explode if hitting a player or enemy, and not ourself
        if(collision.collider != myCollider && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy")))
            Detonate();
    }

    private void Detonate()
    {
        //Runs for each collider within range of the grenade.
        foreach(var hit in Physics2D.OverlapCircleAll(transform.position, range))
        {
            //Applies damage if a damagescript is found.
            hit.GetComponent<HealthScript>()?.TakeDamage(damage);
        }

        //Destroys the grenade
        Destroy(gameObject);  
    }
}
