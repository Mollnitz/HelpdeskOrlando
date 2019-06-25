using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    Rigidbody2D playerBody;

    [Range(3f, 10f)]
    public float speedAmp = 5f;
    


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        var inputs = SetInputs();
        HandleMovement(inputs);

    }

    private void HandleMovement(Vector2 inputs)
    {
        playerBody.velocity = inputs;
        
    }

    Vector2 SetInputs()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedAmp;
    }

}
