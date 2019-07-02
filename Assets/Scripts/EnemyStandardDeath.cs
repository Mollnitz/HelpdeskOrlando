using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class EnemyStandardDeath : MonoBehaviour
{
    [SerializeField] [Range(500f, 1500f)]
    float PointWorth = 1000f;

    private bool died = false;

    private void Start()
    {
        
        GetComponent<HealthScript>().deathEvent.AddListener(() => {
            if (!died)
            {
                died = true;
            }
            else
            {
                return;
            }
                
            Debug.Log(gameObject.name);
            

            if(GetComponent<EnemyShootingManagement>().weapon == null)
            {
                GameManager.EnemySemaphor--;
                GameManager.PointEvent.Invoke(PointWorth);
                Destroy(this.gameObject);
            }
            else
            {
                GameManager.EnemySemaphor--;
                GameManager.PointEvent.Invoke(PointWorth * GetComponent<EnemyShootingManagement>().weapon.PointMultiplier);
                Destroy(this.gameObject);
            }
            });
    }
}
