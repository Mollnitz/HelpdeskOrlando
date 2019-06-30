using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class EnemyStandardDeath : MonoBehaviour
{
    [SerializeField] [Range(500f, 1500f)]
    float PointWorth = 1000f;
    private void Start()
    {
        
        GetComponent<HealthScript>().deathEvent.AddListener(() => {
            GameManager.EnemySemaphor--;

            if(GetComponent<EnemyShootingManagement>().weapon == null)
            {
                GameManager.PointEvent.Invoke(PointWorth);
            }
            else
            {
                GameManager.PointEvent.Invoke(PointWorth * GetComponent<EnemyShootingManagement>().weapon.PointMultiplier);
            }
             

            Destroy(this.gameObject);
            }
        );
        
    }
}
