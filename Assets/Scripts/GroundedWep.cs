using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class GroundedWep : MonoBehaviour
{
    [SerializeField]
    public ShootSO so;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = so.GroundRepresentation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            //Raise event
            GameManager.pickupEvent.Invoke(so);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            GameManager.enemyPickupEvent.Invoke(collision.gameObject, so);
            Destroy(this.gameObject);
        }
    }

}
