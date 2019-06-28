using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class PlayerEventInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthScript>().healEvent.AddListener(x => GameManager.PlayerHealEvent.Invoke(x));    
        GetComponent<HealthScript>().damageEvent.AddListener(x => GameManager.PlayerDamageEvent.Invoke(x));    
    }
}
