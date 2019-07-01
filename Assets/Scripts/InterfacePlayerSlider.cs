using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using System;

public class InterfacePlayerSlider : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.fillRect.gameObject.SetActive(false);

        SetStateAllChildren(false);

        GameManager.pickupEvent.AddListener(x =>
        {
            SetStateAllChildren(true);
            slider.maxValue = x.so.FireCooldown;
        });

        GameManager.shootEvent.AddListener(x =>
        {
            //Just to be sure.
            slider.maxValue = x.FireCooldown;
            slider.value = 0f;
            slider.fillRect.gameObject.SetActive(true);
            StartCoroutine( RunSlider(slider.maxValue));
        });

        GameManager.discardEvent.AddListener(x =>
        {
            SetStateAllChildren();
        });
    }

    private void SetStateAllChildren(bool state = false)
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(state);
        }
    }

    private IEnumerator RunSlider(float reloadTime)
    {
        float activeTime = 0f;
        while(activeTime < reloadTime)
        {
            yield return new WaitForEndOfFrame();
            activeTime += Time.deltaTime;
            slider.value = activeTime;
        }
        slider.fillRect.gameObject.SetActive(false);
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
