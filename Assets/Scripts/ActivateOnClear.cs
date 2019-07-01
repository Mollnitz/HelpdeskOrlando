using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class ActivateOnClear : MonoBehaviour
{
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        GameManager.levelClear.AddListener(() => {
            col.enabled = true;
            GetComponent<AudioSource>().Play();
        }
        );
    }
    
}
