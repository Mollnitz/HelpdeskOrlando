﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManagement : MonoBehaviour
{
    [SerializeField]
    ShootSO carriedWep; //Current weapon

    [SerializeField]
    GameObject groundWep;

    // Start is called before the first frame update
    void Start()
    {
        //Equip weapon on pickup
        GameManager.pickupEvent.AddListener(x => carriedWep = x);

        //Throw weapon away on button input.
        GameManager.discardEvent.AddListener(x =>
        {
            GameObject wep = GameObject.Instantiate(groundWep, transform.position - (transform.right * 2f), Quaternion.identity);
            wep.GetComponent<GroundedWep>().so = x;
            carriedWep = null;
        });

    }

    // Update is called once per frame
    void Update()
    {
        if(carriedWep != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject shot = GameObject.Instantiate(carriedWep.Shot, transform.position - (transform.right * 1f), Quaternion.identity);
                carriedWep.shootAction(shot.GetComponent<Rigidbody2D>(), -transform.right);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                GameManager.discardEvent.Invoke(carriedWep);
            }
        }
        


    }



}
