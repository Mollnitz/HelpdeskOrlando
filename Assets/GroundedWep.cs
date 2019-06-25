using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedWep : MonoBehaviour
{

    ShootSO so;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = so.GroundRepresentation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
