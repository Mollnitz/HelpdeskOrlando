using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class InterfacePlayerPickup : MonoBehaviour
{

    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        GameManager.pickupEvent.AddListener(x => {
            img.sprite = x.GroundRepresentation;
            img.color = Color.white;
            }
        );
        GameManager.discardEvent.AddListener(x => {
            img.sprite = null;
            img.color = Color.clear;
            }
        );
    }

}
