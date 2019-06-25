using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var camvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camvec.z = 0f;
        transform.rotation = Quaternion.LookRotation(camvec - transform.position, Vector3.up) * Quaternion.Euler(0, 90, 0) ;
    }
}
