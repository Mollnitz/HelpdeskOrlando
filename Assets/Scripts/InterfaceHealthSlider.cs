using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;

public class InterfaceHealthSlider : MonoBehaviour
{
    private float StartingHealth;
    private float CurrentHealth;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        StartingHealth = GameManager.instance.playerRef.GetComponent<HealthScript>().health;
        CurrentHealth = StartingHealth;
        GameManager.PlayerHealEvent.AddListener(x => UpdateHealth(x));
        GameManager.PlayerDamageEvent.AddListener(x => UpdateHealth(x));
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        slider.value = CurrentHealth / StartingHealth;
    }

    private void UpdateHealth(float value)
    {
        CurrentHealth = value;
        if(CurrentHealth > StartingHealth)
        {
            StartingHealth = CurrentHealth;
        }
        UpdateSlider();
    }

    
}
