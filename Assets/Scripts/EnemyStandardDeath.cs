using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardDeath : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HealthScript>().deathEvent.AddListener(() => Destroy(this.gameObject));
    }
}
