using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    List<Collider2D> colliders;
    private void Start()
    {
        colliders = new List<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            GetComponentInParent<DoorScript>().OpenDoor();
            colliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            GetComponentInParent<DoorScript>().CloseDoor();
            colliders.Remove(collision);
        }
    }

    public bool TriggerCheck()
    {
        return colliders.Count == 0;
    }

}
