using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{

    internal UnityEvent deathEvent;
    internal FloatEvent healEvent;
    internal FloatEvent damageEvent;


    [Range(20, 200f)]
    public float health = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        deathEvent = new UnityEvent();
        healEvent = new FloatEvent();
        damageEvent = new FloatEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float Damage)
    {
        health -= Damage;
        if(health <= 0f)
        {
            deathEvent.Invoke();
        }
        else
        {
            damageEvent.Invoke(health);
        }
    }
}
