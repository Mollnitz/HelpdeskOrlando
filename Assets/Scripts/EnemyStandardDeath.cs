using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class EnemyStandardDeath : MonoBehaviour
{
    private void Start()
    {
        
        GetComponent<HealthScript>().deathEvent.AddListener(() => {
            GameManager.EnemySemaphor--;
            Destroy(this.gameObject);
            }
        );
        
    }
}
