using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public class ShootingManagement : MonoBehaviour
{
    [SerializeField]
    ShootSO carriedWep; //Current weapon

    [SerializeField]
    GameObject groundWep;

    bool canFire = true;


    // Start is called before the first frame update
    void Start()
    {
        //Equip weapon on pickup
        GameManager.pickupEvent.AddListener(x => carriedWep = x.so);

        //Throw weapon away on button input.
        GameManager.discardEvent.AddListener(x =>
        {
            GameObject wep = GameObject.Instantiate(groundWep, transform.position + (transform.up * 2f), Quaternion.identity);
            wep.GetComponent<GroundedWep>().so = x;
            carriedWep = null;
        });

        //Shoot weapon on button input
        GameManager.shootEvent.AddListener(x =>
        {
            canFire = false;
            StartCoroutine(ResetFire(x.FireCooldown));
            GameObject shot = GameObject.Instantiate(x.Shot, transform.position + (transform.up * 1f), Quaternion.identity);
            x.Shoot(shot.GetComponent<Rigidbody2D>(), transform.up);
        });

        
    }

    private IEnumerator ResetFire(float fireCooldown)
    {
        //This could be expanded to handle buffering inputs.
        yield return new WaitForSeconds(fireCooldown);
        if (Input.GetButton("Fire1"))
        {
            GameManager.shootEvent.Invoke(carriedWep);
        }
        else
        {
            canFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(carriedWep != null)
        {
            if (Input.GetButtonDown("Fire1") && canFire)
            {
                GameManager.shootEvent.Invoke(carriedWep);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                GameManager.discardEvent.Invoke(carriedWep);
            }
        }
        


    }



}
